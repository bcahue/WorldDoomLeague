using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Domain.Game;
using WorldDoomLeague.Domain.MatchModel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WorldDoomLeague.Application.ConfigModels;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.Matches.Commands.ProcessMatch
{
    public partial class ProcessMatchCommand : IRequest<uint>
    {
        public uint MatchId { get; set; }
        public bool FlipTeams { get; set; }
        public IList<RoundObject> GameRounds { get; set; }
    }

    public class ProcessMatchCommandHandler : IRequestHandler<ProcessMatchCommand, uint>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGetMatchJson _getMatchJson;
        private readonly IOptionsSnapshot<DataDirectories> _optionsDelegate;

        public ProcessMatchCommandHandler(IApplicationDbContext context, IGetMatchJson getMatchJson, IOptionsSnapshot<DataDirectories> optionsDelegate)
        {
            _context = context;
            _getMatchJson = getMatchJson;
            _optionsDelegate = optionsDelegate;
        }

        public async Task<uint> Handle(ProcessMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Games.Where(w => w.IdGame == request.MatchId).FirstOrDefaultAsync(cancellationToken);

            // Get every round into their objects via json.

            List<Round> rounds = new List<Round>();
            List<RoundResults> roundResults = new List<RoundResults>();

            foreach (var round in request.GameRounds)
            {
                rounds.Add(await _getMatchJson.GetRoundObject(_optionsDelegate.Value.JsonMatchDirectory, round.RoundFileName));
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                var game = await _context.Games.Where(w => w.IdGame == request.MatchId).FirstOrDefaultAsync();

                // Loop thru each round, replace player names with player ids, and determine round winners.
                for (var i = 0; i < request.GameRounds.Count; i++)
                {
                    IDictionary<string, uint> redPlayerIdLookup = new Dictionary<string, uint>();
                    IDictionary<string, uint> bluePlayerIdLookup = new Dictionary<string, uint>();

                    for (var j = 0; j < rounds[i].RedTeamStats.TeamPlayers.Count; j++)
                    {
                        if (request.FlipTeams)
                        {
                            redPlayerIdLookup.Add(rounds[i].RedTeamStats.TeamPlayers[j], request.GameRounds[i].BlueTeamPlayerIds[j]);
                        }
                        else
                        {
                            redPlayerIdLookup.Add(rounds[i].RedTeamStats.TeamPlayers[j], request.GameRounds[i].RedTeamPlayerIds[j]);
                        }
                    }

                    for (var j = 0; j < rounds[i].BlueTeamStats.TeamPlayers.Count; j++)
                    {
                        if (request.FlipTeams)
                        {
                            bluePlayerIdLookup.Add(rounds[i].BlueTeamStats.TeamPlayers[j], request.GameRounds[i].RedTeamPlayerIds[j]);
                        }
                        else
                        {
                            bluePlayerIdLookup.Add(rounds[i].BlueTeamStats.TeamPlayers[j], request.GameRounds[i].BlueTeamPlayerIds[j]);
                        }
                    }

                    // Calculate winner
                    var winner = GameHelper.CalculateRoundResult(
                        request.FlipTeams ? rounds[i].BlueTeamStats.Points : rounds[i].RedTeamStats.Points,
                        request.FlipTeams ? rounds[i].RedTeamStats.Points : rounds[i].BlueTeamStats.Points,
                        redPlayerIdLookup.Values.ToList(),
                        bluePlayerIdLookup.Values.ToList());

                    roundResults.Add(winner);

                    var round = new Domain.Entities.Rounds
                    {
                        FkIdGame = game.IdGame,
                        FkIdMap = request.GameRounds[i].Map,
                        FkIdSeason = game.FkIdSeason,
                        FkIdWeek = game.FkIdWeek,
                        RoundNumber = (uint)i + 1,
                        RoundParseVersion = (ushort)rounds[i].MetaData.ParserVersion,
                        RoundDatetime = rounds[i].MetaData.Date.UtcDateTime,
                        RoundTicsDuration = (uint)rounds[i].MetaData.DurationTics,
                        RoundWinner = winner.ToString()
                    };

                    await _context.SaveChangesAsync(cancellationToken);

                    // Assign stat entities to each player.
                    foreach (var player in rounds[i].RedTeamStats.TeamPlayers)
                    {
                        var playerStats = rounds[i].PlayerStats.Where(w => w.Name == player).FirstOrDefault();

                        var statsroundEntity = GameHelper.CreateStatRound(playerStats,
                            request.MatchId,
                            request.GameRounds[i].Map,
                            game.FkIdSeason,
                            game.FkIdWeek,
                            request.FlipTeams ? game.FkIdTeamRed : game.FkIdTeamBlue,
                            redPlayerIdLookup[player],
                            round.IdRound,
                            request.FlipTeams ? LogFileEnums.Teams.Blue : LogFileEnums.Teams.Red);
                    }

                    foreach (var player in rounds[i].BlueTeamStats.TeamPlayers)
                    {
                        var playerStats = rounds[i].PlayerStats.Where(w => w.Name == player).FirstOrDefault();

                        var statsroundEntity = GameHelper.CreateStatRound(playerStats,
                            request.MatchId,
                            request.GameRounds[i].Map,
                            game.FkIdSeason,
                            game.FkIdWeek,
                            request.FlipTeams ? game.FkIdTeamBlue : game.FkIdTeamRed,
                            redPlayerIdLookup[player],
                            round.IdRound,
                            request.FlipTeams ? LogFileEnums.Teams.Red : LogFileEnums.Teams.Blue);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }

                _context.Database.CommitTransaction();
                // This transaction is automatically rolled back (and never committed) on error.
                //_context.Database.RollbackTransaction();
            }
            // Finalize, create all necessary stats objects, and determine who won/lost the game, and create player records for it accordingly.

            return match.IdGame;
        }
    }
}
