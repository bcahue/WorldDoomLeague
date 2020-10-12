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

                    _context.Rounds.Add(round);

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
                            request.FlipTeams ? game.FkIdTeamBlue : game.FkIdTeamRed,
                            redPlayerIdLookup[player],
                            round.IdRound,
                            request.FlipTeams ? LogFileEnums.Teams.Blue : LogFileEnums.Teams.Red);

                        _context.StatsRounds.Add(statsroundEntity);

                        var accuracyEntities = GameHelper.GetAccuracyEntities(playerStats.AccuracyList, redPlayerIdLookup[player], round.IdRound);

                        _context.StatsAccuracyData.AddRange(accuracyEntities);

                        var flagAccuracy = GameHelper.GetAccuracyWithFlagEntities(playerStats.AccuracyWithFlagList, redPlayerIdLookup[player], round.IdRound);

                        _context.StatsAccuracyWithFlagData.AddRange(flagAccuracy);

                        var damage = GameHelper.GetDamageEntities(playerStats.DamageList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound);

                        _context.StatsDamageData.AddRange(damage);

                        var damageWithFlag = GameHelper.GetDamageWithFlagEntities(playerStats.DamageWithFlagList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound);

                        _context.StatsDamageWithFlagData.AddRange(damageWithFlag);

                        var kills = GameHelper.GetKillsEntities(playerStats.KillsList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound);

                        _context.StatsKillData.AddRange(kills);

                        var killsWithFlag = GameHelper.GetKillsWithFlagEntities(playerStats.CarrierKillList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound);

                        _context.StatsKillCarrierData.AddRange(killsWithFlag);

                        var pickups = GameHelper.GetPickupsEntities(playerStats.PickupList, redPlayerIdLookup[player], round.IdRound);

                        _context.StatsPickupData.AddRange(pickups);

                        var roundplayer = new RoundPlayers
                        {
                            FkIdGame = game.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = request.FlipTeams ? game.FkIdTeamBlue : game.FkIdTeamRed,
                            FkIdPlayer = redPlayerIdLookup[player]
                        };

                        _context.RoundPlayers.Add(roundplayer);

                        var roundRecord = new PlayerRoundRecord
                        {
                            FkIdGame = game.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = request.FlipTeams ? game.FkIdTeamBlue : game.FkIdTeamRed,
                            FkIdPlayer = redPlayerIdLookup[player],
                            FkIdStatsRound = statsroundEntity.IdStatsRound,
                            Win = winner.RoundResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0,
                            Tie = winner.RoundResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0,
                            Loss = winner.RoundResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0,
                            AsCaptain = WasPlayerCaptain(redPlayerIdLookup[player], request.FlipTeams ? game.FkIdTeamBlue : game.FkIdTeamRed, cancellationToken).Result
                        };
                    }

                    foreach (var player in rounds[i].BlueTeamStats.TeamPlayers)
                    {
                        var playerStats = rounds[i].PlayerStats.Where(w => w.Name == player).FirstOrDefault();

                        var statsroundEntity = GameHelper.CreateStatRound(playerStats,
                            request.MatchId,
                            request.GameRounds[i].Map,
                            game.FkIdSeason,
                            game.FkIdWeek,
                            request.FlipTeams ? game.FkIdTeamRed : game.FkIdTeamBlue,
                            bluePlayerIdLookup[player],
                            round.IdRound,
                            request.FlipTeams ? LogFileEnums.Teams.Red : LogFileEnums.Teams.Blue);

                        _context.StatsRounds.Add(statsroundEntity);

                        var accuracyEntities = GameHelper.GetAccuracyEntities(playerStats.AccuracyList, bluePlayerIdLookup[player], round.IdRound);

                        _context.StatsAccuracyData.AddRange(accuracyEntities);

                        var flagAccuracy = GameHelper.GetAccuracyWithFlagEntities(playerStats.AccuracyWithFlagList, bluePlayerIdLookup[player], round.IdRound);

                        _context.StatsAccuracyWithFlagData.AddRange(flagAccuracy);

                        var damage = GameHelper.GetDamageEntities(playerStats.DamageList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound);

                        _context.StatsDamageData.AddRange(damage);

                        var damageWithFlag = GameHelper.GetDamageWithFlagEntities(playerStats.DamageWithFlagList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound);

                        _context.StatsDamageWithFlagData.AddRange(damageWithFlag);

                        var kills = GameHelper.GetKillsEntities(playerStats.KillsList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound);

                        _context.StatsKillData.AddRange(kills);

                        var killsWithFlag = GameHelper.GetKillsWithFlagEntities(playerStats.CarrierKillList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound);

                        _context.StatsKillCarrierData.AddRange(killsWithFlag);

                        var pickups = GameHelper.GetPickupsEntities(playerStats.PickupList, bluePlayerIdLookup[player], round.IdRound);

                        _context.StatsPickupData.AddRange(pickups);

                        var roundplayer = new RoundPlayers
                        {
                            FkIdGame = game.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = request.FlipTeams ? game.FkIdTeamRed : game.FkIdTeamBlue,
                            FkIdPlayer = bluePlayerIdLookup[player]
                        };

                        _context.RoundPlayers.Add(roundplayer);

                        var roundRecord = new PlayerRoundRecord
                        {
                            FkIdGame = game.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = request.FlipTeams ? game.FkIdTeamRed : game.FkIdTeamBlue,
                            FkIdPlayer = bluePlayerIdLookup[player],
                            FkIdStatsRound = statsroundEntity.IdStatsRound,
                            Win = winner.RoundResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0,
                            Tie = winner.RoundResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0,
                            Loss = winner.RoundResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0,
                            AsCaptain = WasPlayerCaptain(bluePlayerIdLookup[player], request.FlipTeams ? game.FkIdTeamRed : game.FkIdTeamBlue, cancellationToken).Result
                        };
                    }

                    // Flag assists and captures.
                    // Improved to include touch times, so flag capture length can be recorded.
                    foreach (var flagCapEvent in rounds[i].FlagAssistTable.FlagTouchCaptures)
                    {
                        IDictionary<string, uint> captureIdList;
                        LogFileEnums.Teams team = LogFileEnums.Teams.None;

                        if (request.FlipTeams)
                        {
                            if (flagCapEvent.Team == LogFileEnums.Teams.Red)
                            {
                                team = LogFileEnums.Teams.Blue;
                            }
                            else
                            {
                                team = LogFileEnums.Teams.Red;
                            }
                        }

                        if (team == LogFileEnums.Teams.Blue)
                        {
                            captureIdList = bluePlayerIdLookup;
                        }
                        else if (team == LogFileEnums.Teams.Red)
                        {
                            captureIdList = redPlayerIdLookup;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }

                        uint capturePosition = 1;
                        KeyValuePair<string, uint> lastAssister = new KeyValuePair<string, uint>();
                        List<RoundFlagTouchCaptures> flagAssistsAndCaptures = new List<RoundFlagTouchCaptures>();

                        foreach (var assist in flagCapEvent.FlagAssists)
                        {
                            lastAssister = new KeyValuePair<string, uint>(assist.PlayerName, captureIdList[assist.PlayerName]);

                            var flagAssist = new RoundFlagTouchCaptures
                            {
                                FkIdGame = game.IdGame,
                                FkIdPlayer = lastAssister.Value,
                                FkIdRound = round.IdRound,
                                FkIdTeam = team == LogFileEnums.Teams.Red ? game.FkIdTeamRed : game.FkIdTeamBlue,
                                CaptureNumber = capturePosition,
                                Gametic = (uint)assist.FlagTouchTimeTics,
                                Team = team == LogFileEnums.Teams.Red ? "r" : "b"
                            };

                            capturePosition++;

                            flagAssistsAndCaptures.Add(flagAssist);
                        }

                        var capture = new RoundFlagTouchCaptures
                        {
                            FkIdGame = game.IdGame,
                            FkIdPlayer = lastAssister.Value,
                            FkIdRound = round.IdRound,
                            FkIdTeam = team == LogFileEnums.Teams.Red ? game.FkIdTeamRed : game.FkIdTeamBlue,
                            CaptureNumber = capturePosition,
                            Gametic = (uint)flagCapEvent.TimeCapturedTics,
                            Team = team == LogFileEnums.Teams.Red ? "r" : "b"
                        };

                        flagAssistsAndCaptures.Add(capture);

                        _context.RoundFlagTouchCaptures.AddRange(flagAssistsAndCaptures);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Determine game winner
                GameRecordKeeper gameWinner = new GameRecordKeeper(roundResults);

                // Create player game records and gameplayers
                var totalRoundPlayers = await _context.RoundPlayers.Where(w => w.FkIdGame == game.IdGame).ToListAsync(cancellationToken);
                var gamePlayers = totalRoundPlayers.GroupBy(g => g.FkIdPlayer).Select(s => s.First()).ToList();
                var gamePlayerList = new List<GamePlayers>();
                var playerGameRecordList = new List<PlayerGameRecord>();

                foreach (var player in gamePlayers)
                {
                    var gamePlayer = new GamePlayers
                    {
                        FkIdWeek = player.FkIdWeek,
                        FkIdTeam = player.FkIdTeam,
                        FkIdSeason = player.FkIdSeason,
                        FkIdGame = player.FkIdGame,
                        FkIdPlayer = player.FkIdPlayer
                    };

                    var gameRecord = new PlayerGameRecord
                    {
                        FkIdWeek = player.FkIdWeek,
                        FkIdTeam = player.FkIdTeam,
                        FkIdSeason = player.FkIdSeason,
                        FkIdGame = player.FkIdGame,
                        FkIdPlayer = player.FkIdPlayer,
                        Win = gameWinner.GameResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0,
                        Tie = gameWinner.GameResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0,
                        Loss = gameWinner.GameResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0,
                        AsCaptain = WasPlayerCaptain(player.FkIdPlayer, player.FkIdTeam, cancellationToken).Result,
                    };

                    gamePlayerList.Add(gamePlayer);
                    playerGameRecordList.Add(gameRecord);
                }

                _context.GamePlayers.AddRange(gamePlayerList);
                _context.PlayerGameRecords.AddRange(playerGameRecordList);

                // Create game team stats.

                var redTeamStats = request.FlipTeams ? rounds.Select(s => s.BlueTeamStats) : rounds.Select(s => s.RedTeamStats);
                var blueTeamStats = request.FlipTeams ? rounds.Select(s => s.RedTeamStats) : rounds.Select(s => s.BlueTeamStats);
                var redPlayers = request.FlipTeams ? rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Blue) : rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Red);
                var bluePlayers = request.FlipTeams ? rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Red) : rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Blue);

                var gameTeamStatsRed = GameHelper.CalculateGameTeamStats(
                    game.IdGame,
                    game.FkIdSeason,
                    game.FkIdWeek,
                    game.FkIdTeamRed,
                    game.FkIdTeamBlue,
                    (uint)rounds.Count,
                    (uint)rounds.Sum(s => s.MetaData.DurationTics),
                    redTeamStats,
                    redPlayers,
                    (uint)blueTeamStats.Sum(s => s.Points),
                    LogFileEnums.Teams.Red,
                    gameWinner);

                var gameTeamStatsBlue = GameHelper.CalculateGameTeamStats(
                    game.IdGame,
                    game.FkIdSeason,
                    game.FkIdWeek,
                    game.FkIdTeamBlue,
                    game.FkIdTeamRed,
                    (uint)rounds.Count,
                    (uint)rounds.Sum(s => s.MetaData.DurationTics),
                    blueTeamStats,
                    bluePlayers,
                    (uint)redTeamStats.Sum(s => s.Points),
                    LogFileEnums.Teams.Blue,
                    gameWinner);

                _context.GameTeamStats.Add(gameTeamStatsBlue);
                _context.GameTeamStats.Add(gameTeamStatsRed);

                _context.Database.CommitTransaction();
                // This transaction is automatically rolled back (and never committed) on error.
                //_context.Database.RollbackTransaction();

                // TODO:
                // 1. Create kill/death maps here and send to mongo.
                // 2. Create game event file and send to mongo.
            }

            return match.IdGame;
        }

        private async Task<byte> WasPlayerCaptain(uint playerId, uint teamId, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.Where(w => w.IdTeam == teamId).FirstOrDefaultAsync(cancellationToken);
            var currentCaptain = team.FkIdPlayerCaptain;

            if (currentCaptain == playerId)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
