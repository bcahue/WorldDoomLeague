using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorldDoomLeague.Application.Common.Exceptions;
using System.Collections.Generic;
using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Application.Common.Models;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class GetMatchSummaryByMatchIdQuery : IRequest<MatchSummaryVm>
    {
        public uint MatchId { get; }

        public GetMatchSummaryByMatchIdQuery(uint matchId)
        {
            MatchId = matchId;
        }
    }

    public class GetMatchSummaryByMatchIdQueryHandler : IRequestHandler<GetMatchSummaryByMatchIdQuery, MatchSummaryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetMatchSummaryByMatchIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MatchSummaryVm> Handle(GetMatchSummaryByMatchIdQuery request, CancellationToken cancellationToken)
        {
            var gameInfo = await _context.Games
                    .Where(w => w.IdGame == request.MatchId)
                    .Include(i => i.StatsRounds)
                        .ThenInclude(ti => ti.FkIdMapNavigation)
                    .Include(i => i.Rounds)
                    .Include(i => i.FkIdSeasonNavigation)
                    .Include(i => i.FkIdTeamBlueNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                    .Include(i => i.GamePlayers)
                        .ThenInclude(ti => ti.FkIdPlayerNavigation)
                    .FirstOrDefaultAsync(cancellationToken)
                    ??
                    throw new NotFoundException(string.Format("Game id {0} was not found in the database.", request.MatchId));

            // Create maps played list
            List<MatchMapsPlayedDto> mapsPlayed = new List<MatchMapsPlayedDto>();

            var maps = gameInfo.StatsRounds.Select(s => s.FkIdMapNavigation).Distinct();

            foreach (var map in maps)
            {
                mapsPlayed.Add(new MatchMapsPlayedDto()
                {
                    MapId = map.IdMap,
                    MapNumber = map.MapNumber,
                    MapPack = map.MapPack,
                    MapName = map.MapName
                });
            }

            // Create demo list
            List<DemoDto> demoList = new List<DemoDto>();

            foreach (var player in gameInfo.GamePlayers)
            {
                demoList.Add(new DemoDto()
                { 
                    PlayerId = player.FkIdPlayerNavigation.Id,
                    PlayerName = player.FkIdPlayerNavigation.PlayerName,
                    DemoFilePath = player.DemoFilePath,
                    DemoLost = player.DemoNotTaken == "y",
                });
            }

            // Create the line score
            MatchLineScoreDto matchLineScore = new MatchLineScoreDto()
            { 
                BlueTeamName = gameInfo.FkIdTeamBlueNavigation.TeamName,
                RedTeamName  = gameInfo.FkIdTeamRedNavigation.TeamName,
            };

            var rounds = gameInfo.Rounds.Distinct().ToList();

            List<ScoreDto> roundScore = new List<ScoreDto>();

            var redScore = 0;
            var blueScore = 0;
            string winner = "";
            // Per round...
            foreach (var round in rounds)
            {
                var roundStats = gameInfo.StatsRounds.Where(w => w.FkIdRound == round.IdRound);
                roundScore.Add(new ScoreDto()
                {
                    Round = string.Format("Round {0}", round.RoundNumber.ToString()),
                    BlueScore = roundStats.Where(w => w.Team == "b").Sum(s => (s.TotalCaptures + s.TotalPickupCaptures)),
                    RedScore  = roundStats.Where(w => w.Team == "r").Sum(s => (s.TotalCaptures + s.TotalPickupCaptures)),
                    RoundWinner = round.RoundWinner
                });

                switch (round.RoundWinner)
                {
                    case "r":
                        redScore++;
                        break;
                    case "b":
                        blueScore++;
                        break;
                }
            }

            if (redScore > blueScore)
            {
                winner = "r";
            } else if (redScore < blueScore)
            {
                winner = "b";
            } else if (redScore == blueScore)
            {
                winner = "t";
            }

            matchLineScore.RoundScore = roundScore;

            // And final
            matchLineScore.MatchResult = new MatchResultDto()
            {
                RedRoundScore = redScore,
                BlueRoundScore = blueScore,
                GameWinner = winner
            };

            // Get the final team and player box score.
            MatchFinalBoxScoreDto matchFinalBoxScore = new MatchFinalBoxScoreDto
            {
                BlueTeamFinalBoxScore = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdTeam == gameInfo.FkIdTeamBlue)),
                RedTeamFinalBoxScore = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdTeam == gameInfo.FkIdTeamRed))
            };

            var players = gameInfo.GamePlayers.Distinct().ToList();

            List<GamePlayersDto> blueGamePlayers = new List<GamePlayersDto>();
            List<GamePlayersDto> redGamePlayers  = new List<GamePlayersDto>();

            foreach (var player in players)
            {
                if (player.FkIdTeam == gameInfo.FkIdTeamBlue)
                {
                    blueGamePlayers.Add(new GamePlayersDto()
                    {
                        PlayerId = player.FkIdPlayer,
                        PlayerName = player.FkIdPlayerNavigation.PlayerName,
                        MatchStats = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdTeam == gameInfo.FkIdTeamBlue && w.FkIdPlayer == player.FkIdPlayer))
                    });
                }
                if (player.FkIdTeam == gameInfo.FkIdTeamRed)
                {
                    redGamePlayers.Add(new GamePlayersDto()
                    {
                        PlayerId = player.FkIdPlayer,
                        PlayerName = player.FkIdPlayerNavigation.PlayerName,
                        MatchStats = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdTeam == gameInfo.FkIdTeamRed && w.FkIdPlayer == player.FkIdPlayer))
                    });
                }
            }

            matchFinalBoxScore.BlueTeamPlayerFinalBoxScore = blueGamePlayers;
            matchFinalBoxScore.RedTeamPlayerFinalBoxScore  = redGamePlayers;

            // Finally, get the box score by round.

            List<RoundBoxScoreDto> roundBoxScore = new List<RoundBoxScoreDto>();
            var roundnum = 0;
            foreach (var round in rounds)
            {
                List<GamePlayersDto> blueTeamPlayers = new List<GamePlayersDto>();
                List<GamePlayersDto> redTeamPlayers  = new List<GamePlayersDto>();

                foreach (var player in players)
                {
                    if (player.FkIdTeam == gameInfo.FkIdTeamBlue)
                    {
                        blueTeamPlayers.Add(new GamePlayersDto()
                        {
                            PlayerId = player.FkIdPlayer,
                            PlayerName = player.FkIdPlayerNavigation.PlayerName,
                            MatchStats = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdTeam == gameInfo.FkIdTeamBlue 
                            && w.FkIdPlayer == player.FkIdPlayer 
                            && w.FkIdRound == round.IdRound))
                        });
                    }
                    if (player.FkIdTeam == gameInfo.FkIdTeamRed)
                    {
                        redTeamPlayers.Add(new GamePlayersDto()
                        {
                            PlayerId = player.FkIdPlayer,
                            PlayerName = player.FkIdPlayerNavigation.PlayerName,
                            MatchStats = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdTeam == gameInfo.FkIdTeamRed 
                            && w.FkIdPlayer == player.FkIdPlayer 
                            && w.FkIdRound == round.IdRound))
                        });
                    }
                }
                roundnum++;
                roundBoxScore.Add(new RoundBoxScoreDto()
                {
                    RoundNumber = roundnum,
                    RoundTimeTotal = TimeSpan.FromSeconds((double)round.RoundTicsDuration / 35),
                    BlueTeamBoxScore = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdRound == round.IdRound && w.FkIdTeam == gameInfo.FkIdTeamBlueNavigation.IdTeam)),
                    RedTeamBoxScore = Score.SumStats(gameInfo.StatsRounds.Where(w => w.FkIdRound == round.IdRound && w.FkIdTeam == gameInfo.FkIdTeamRedNavigation.IdTeam)),
                    RedTeamPlayerRoundBoxScore = redTeamPlayers,
                    BlueTeamPlayerRoundBoxScore = blueTeamPlayers
                });
            }

            return new MatchSummaryVm
            {
                MatchId         = request.MatchId,
                BlueTeamId      = gameInfo.FkIdTeamBlueNavigation.IdTeam,
                RedTeamId       = gameInfo.FkIdTeamRedNavigation.IdTeam,
                SeasonId        = gameInfo.FkIdSeason,
                RoundsPlayed    = gameInfo.Rounds.Count(),
                MapsPlayed      = mapsPlayed,
                BlueTeamName    = gameInfo.FkIdTeamBlueNavigation.TeamName,
                RedTeamName     = gameInfo.FkIdTeamRedNavigation.TeamName,
                SeasonName      = gameInfo.FkIdSeasonNavigation.SeasonName,
                GameTimeTotal   = TimeSpan.FromSeconds((double)gameInfo.Rounds.Sum(s => s.RoundTicsDuration) / 35),
                DemoList        = demoList,
                LineScore       = matchLineScore,
                FinalBoxScore   = matchFinalBoxScore,
                PerRoundBoxScore= roundBoxScore
            };
        }
    }
}
