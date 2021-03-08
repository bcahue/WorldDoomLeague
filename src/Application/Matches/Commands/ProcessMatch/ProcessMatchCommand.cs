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
                // Loop thru each round, replace player names with player ids, and determine round winners.
                for (var i = 0; i < request.GameRounds.Count; i++)
                {
                    IDictionary<string, uint> redPlayerIdLookup = new Dictionary<string, uint>();
                    IDictionary<string, uint> bluePlayerIdLookup = new Dictionary<string, uint>();

                    // Flip teams should only flip the stats on the round objects.
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
                        FkIdGame = match.IdGame,
                        FkIdMap = request.GameRounds[i].Map,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
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
                            match.FkIdSeason,
                            match.FkIdWeek,
                            match.FkIdTeamRed,
                            redPlayerIdLookup[player],
                            round.IdRound,
                            LogFileEnums.Teams.Red);

                        _context.StatsRounds.Add(statsroundEntity);

                        var accuracyEntities = GameHelper.GetAccuracyEntities(playerStats.AccuracyList, redPlayerIdLookup[player], round.IdRound, match.IdGame);

                        _context.StatsAccuracyData.AddRange(accuracyEntities);

                        var flagAccuracy = GameHelper.GetAccuracyWithFlagEntities(playerStats.AccuracyWithFlagList, redPlayerIdLookup[player], round.IdRound, match.IdGame);

                        _context.StatsAccuracyWithFlagData.AddRange(flagAccuracy);

                        var damage = GameHelper.GetDamageEntities(playerStats.DamageList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsDamageData.AddRange(damage);

                        var damageWithFlag = GameHelper.GetDamageWithFlagEntities(playerStats.DamageWithFlagList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsDamageWithFlagData.AddRange(damageWithFlag);

                        var kills = GameHelper.GetKillsEntities(playerStats.KillsList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsKillData.AddRange(kills);

                        var killsWithFlag = GameHelper.GetFlagCarrierKillsEntities(playerStats.CarrierKillList, redPlayerIdLookup[player], bluePlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsKillCarrierData.AddRange(killsWithFlag);

                        var pickups = GameHelper.GetPickupsEntities(playerStats.PickupList, redPlayerIdLookup[player], round.IdRound, match.IdGame);

                        _context.StatsPickupData.AddRange(pickups);

                        await _context.SaveChangesAsync(cancellationToken);

                        var roundplayer = new RoundPlayers
                        {
                            FkIdGame = match.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = match.FkIdTeamRed,
                            FkIdPlayer = redPlayerIdLookup[player],
                            RoundTicsDuration = (uint)rounds[i].MetaData.DurationTics
                        };

                        _context.RoundPlayers.Add(roundplayer);

                        var roundRecord = new PlayerRoundRecord
                        {
                            FkIdGame = match.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = match.FkIdTeamRed,
                            FkIdPlayer = redPlayerIdLookup[player],
                            FkIdStatsRound = statsroundEntity.IdStatsRound,
                            Win = winner.RoundResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0,
                            Tie = winner.RoundResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0,
                            Loss = winner.RoundResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0,
                            AsCaptain = WasPlayerCaptain(redPlayerIdLookup[player], match.FkIdTeamRed, cancellationToken).Result
                        };

                        _context.PlayerRoundRecords.Add(roundRecord);

                        await _context.SaveChangesAsync(cancellationToken);

                        List<PlayerRoundTeammate> teammateList = new List<PlayerRoundTeammate>();
                        List<PlayerRoundOpponent> opponentList = new List<PlayerRoundOpponent>();

                        foreach (var teammate in redPlayerIdLookup)
                        {
                            if (teammate.Value != redPlayerIdLookup[player])
                            {
                                var roundTeammate = new PlayerRoundTeammate
                                {
                                    FkIdGame = match.IdGame,
                                    FkIdRound = round.IdRound,
                                    FkIdWeek = round.FkIdWeek,
                                    FkIdSeason = round.FkIdSeason,
                                    FkIdTeam = match.FkIdTeamRed,
                                    FkIdPlayer = redPlayerIdLookup[player],
                                    FkIdTeammate = teammate.Value,
                                    FkIdPlayerRoundRecord = roundRecord.RoundRecordID
                                };

                                teammateList.Add(roundTeammate);
                            }
                        }

                        foreach (var opponent in bluePlayerIdLookup)
                        {
                            var roundOpponent = new PlayerRoundOpponent
                            {
                                FkIdGame = match.IdGame,
                                FkIdRound = round.IdRound,
                                FkIdWeek = round.FkIdWeek,
                                FkIdSeason = round.FkIdSeason,
                                FkIdTeam = match.FkIdTeamRed,
                                FkIdPlayer = redPlayerIdLookup[player],
                                FkIdOpponent = opponent.Value,
                                FkIdPlayerRoundRecord = roundRecord.RoundRecordID
                            };

                            opponentList.Add(roundOpponent);
                        }

                        _context.PlayerRoundTeammates.AddRange(teammateList);
                        _context.PlayerRoundOpponents.AddRange(opponentList);
                    }

                    foreach (var player in rounds[i].BlueTeamStats.TeamPlayers)
                    {
                        var playerStats = rounds[i].PlayerStats.Where(w => w.Name == player).FirstOrDefault();

                        var statsroundEntity = GameHelper.CreateStatRound(playerStats,
                            request.MatchId,
                            request.GameRounds[i].Map,
                            match.FkIdSeason,
                            match.FkIdWeek,
                            match.FkIdTeamBlue,
                            bluePlayerIdLookup[player],
                            round.IdRound,
                            LogFileEnums.Teams.Blue);

                        _context.StatsRounds.Add(statsroundEntity);

                        var accuracyEntities = GameHelper.GetAccuracyEntities(playerStats.AccuracyList, bluePlayerIdLookup[player], round.IdRound, match.IdGame);

                        _context.StatsAccuracyData.AddRange(accuracyEntities);

                        var flagAccuracy = GameHelper.GetAccuracyWithFlagEntities(playerStats.AccuracyWithFlagList, bluePlayerIdLookup[player], round.IdRound, match.IdGame);

                        _context.StatsAccuracyWithFlagData.AddRange(flagAccuracy);

                        var damage = GameHelper.GetDamageEntities(playerStats.DamageList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsDamageData.AddRange(damage);

                        var damageWithFlag = GameHelper.GetDamageWithFlagEntities(playerStats.DamageWithFlagList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsDamageWithFlagData.AddRange(damageWithFlag);

                        var kills = GameHelper.GetKillsEntities(playerStats.KillsList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsKillData.AddRange(kills);

                        var killsWithFlag = GameHelper.GetFlagCarrierKillsEntities(playerStats.CarrierKillList, bluePlayerIdLookup[player], redPlayerIdLookup, round.IdRound, match.IdGame);

                        _context.StatsKillCarrierData.AddRange(killsWithFlag);

                        var pickups = GameHelper.GetPickupsEntities(playerStats.PickupList, bluePlayerIdLookup[player], round.IdRound, match.IdGame);

                        _context.StatsPickupData.AddRange(pickups);

                        await _context.SaveChangesAsync(cancellationToken);

                        var roundplayer = new RoundPlayers
                        {
                            FkIdGame = match.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = match.FkIdTeamBlue,
                            FkIdPlayer = bluePlayerIdLookup[player],
                            RoundTicsDuration = (uint)rounds[i].MetaData.DurationTics
                        };

                        _context.RoundPlayers.Add(roundplayer);

                        var roundRecord = new PlayerRoundRecord
                        {
                            FkIdGame = match.IdGame,
                            FkIdMap = round.FkIdMap,
                            FkIdRound = round.IdRound,
                            FkIdSeason = round.FkIdSeason,
                            FkIdWeek = round.FkIdWeek,
                            FkIdTeam = match.FkIdTeamBlue,
                            FkIdPlayer = bluePlayerIdLookup[player],
                            FkIdStatsRound = statsroundEntity.IdStatsRound,
                            Win = winner.RoundResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0,
                            Tie = winner.RoundResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0,
                            Loss = winner.RoundResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0,
                            AsCaptain = WasPlayerCaptain(bluePlayerIdLookup[player], match.FkIdTeamBlue, cancellationToken).Result
                        };

                        _context.PlayerRoundRecords.Add(roundRecord);

                        await _context.SaveChangesAsync(cancellationToken);

                        List<PlayerRoundTeammate> teammateList = new List<PlayerRoundTeammate>();
                        List<PlayerRoundOpponent> opponentList = new List<PlayerRoundOpponent>();

                        foreach (var teammate in bluePlayerIdLookup)
                        {
                            if (teammate.Value != bluePlayerIdLookup[player])
                            {
                                var roundTeammate = new PlayerRoundTeammate
                                {
                                    FkIdGame = match.IdGame,
                                    FkIdRound = round.IdRound,
                                    FkIdWeek = round.FkIdWeek,
                                    FkIdSeason = round.FkIdSeason,
                                    FkIdTeam = match.FkIdTeamBlue,
                                    FkIdPlayer = bluePlayerIdLookup[player],
                                    FkIdTeammate = teammate.Value,
                                    FkIdPlayerRoundRecord = roundRecord.RoundRecordID
                                };

                                teammateList.Add(roundTeammate);
                            }
                        }

                        foreach (var opponent in redPlayerIdLookup)
                        {
                            var roundOpponent = new PlayerRoundOpponent
                            {
                                FkIdGame = match.IdGame,
                                FkIdRound = round.IdRound,
                                FkIdWeek = round.FkIdWeek,
                                FkIdSeason = round.FkIdSeason,
                                FkIdTeam = match.FkIdTeamBlue,
                                FkIdPlayer = bluePlayerIdLookup[player],
                                FkIdOpponent = opponent.Value,
                                FkIdPlayerRoundRecord = roundRecord.RoundRecordID
                            };

                            opponentList.Add(roundOpponent);
                        }

                        _context.PlayerRoundTeammates.AddRange(teammateList);
                        _context.PlayerRoundOpponents.AddRange(opponentList);
                    }

                    // Flag assists and captures.
                    // Improved to include touch times, so flag capture length can be recorded.
                    uint capturePosition = 1;

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
                        else
                        {
                            if (flagCapEvent.Team == LogFileEnums.Teams.Red)
                            {
                                team = LogFileEnums.Teams.Red;
                            }
                            else
                            {
                                team = LogFileEnums.Teams.Blue;
                            }
                        }

                        if (flagCapEvent.Team == LogFileEnums.Teams.Blue)
                        {
                            captureIdList = bluePlayerIdLookup;
                        }
                        else if (flagCapEvent.Team == LogFileEnums.Teams.Red)
                        {
                            captureIdList = redPlayerIdLookup;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }

                        KeyValuePair<string, uint> lastAssister = new KeyValuePair<string, uint>();
                        List<RoundFlagTouchCaptures> flagAssistsAndCaptures = new List<RoundFlagTouchCaptures>();

                        foreach (var assist in flagCapEvent.FlagAssists)
                        {
                            lastAssister = new KeyValuePair<string, uint>(assist.PlayerName, captureIdList[assist.PlayerName]);

                            var flagAssist = new RoundFlagTouchCaptures
                            {
                                FkIdGame = match.IdGame,
                                FkIdPlayer = lastAssister.Value,
                                FkIdRound = round.IdRound,
                                FkIdTeam = team == LogFileEnums.Teams.Red ? match.FkIdTeamRed : match.FkIdTeamBlue,
                                CaptureNumber = capturePosition,
                                Gametic = (uint)assist.FlagTouchTimeTics,
                                Team = team == LogFileEnums.Teams.Red ? "r" : "b"
                            };

                            flagAssistsAndCaptures.Add(flagAssist);
                        }

                        var capture = new RoundFlagTouchCaptures
                        {
                            FkIdGame = match.IdGame,
                            FkIdPlayer = lastAssister.Value,
                            FkIdRound = round.IdRound,
                            FkIdTeam = team == LogFileEnums.Teams.Red ? match.FkIdTeamRed : match.FkIdTeamBlue,
                            CaptureNumber = capturePosition,
                            Gametic = (uint)flagCapEvent.TimeCapturedTics,
                            Team = team == LogFileEnums.Teams.Red ? "r" : "b"
                        };

                        capturePosition++;

                        flagAssistsAndCaptures.Add(capture);

                        _context.RoundFlagTouchCaptures.AddRange(flagAssistsAndCaptures);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Determine game winner
                GameRecordKeeper gameWinner = new GameRecordKeeper(roundResults);

                // Create player game records and gameplayers
                var totalRoundPlayers = await _context.RoundPlayers.Where(w => w.FkIdGame == match.IdGame).ToListAsync(cancellationToken);
                var gamePlayers = totalRoundPlayers.GroupBy(g => g.FkIdPlayer).Select(s => s.First()).ToList();
                var gamePlayerList = new List<GamePlayers>();
                var playerGameRecordList = new List<PlayerGameRecord>();

                // Create player game opponents and teammates.

                // Generate GamePlayers and PlayerGameRecords.
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

                    uint win = 0;
                    uint tie = 0;
                    uint loss = 0;

                    if (gameWinner.GameResult == LogFileEnums.GameResult.RedWin)
                    {
                        if (gameWinner.RedTeamPlayerGameIds.Any(num => num == player.FkIdPlayer))
                        {
                            win = 1;
                            loss = 0;
                            tie = 0;
                        }
                        else if (gameWinner.BlueTeamPlayerGameIds.Any(num => num == player.FkIdPlayer))
                        {
                            win = 0;
                            loss = 1;
                            tie = 0;
                        }
                    }
                    else if (gameWinner.GameResult == LogFileEnums.GameResult.BlueWin)
                    {
                        if (gameWinner.BlueTeamPlayerGameIds.Any(num => num == player.FkIdPlayer))
                        {
                            win = 1;
                            loss = 0;
                            tie = 0;
                        }
                        else if (gameWinner.RedTeamPlayerGameIds.Any(num => num == player.FkIdPlayer))
                        {
                            win = 0;
                            loss = 1;
                            tie = 0;
                        }
                    }
                    else if (gameWinner.GameResult == LogFileEnums.GameResult.TieGame)
                    {
                        win = 0;
                        loss = 0;
                        tie = 1;
                    }

                    var gameRecord = new PlayerGameRecord
                    {
                        FkIdWeek = player.FkIdWeek,
                        FkIdTeam = player.FkIdTeam,
                        FkIdSeason = player.FkIdSeason,
                        FkIdGame = player.FkIdGame,
                        FkIdPlayer = player.FkIdPlayer,
                        Win = win,
                        Tie = tie,
                        Loss = loss,
                        AsCaptain = WasPlayerCaptain(player.FkIdPlayer, player.FkIdTeam, cancellationToken).Result,
                    };

                    List<PlayerGameTeammate> teammateList = new List<PlayerGameTeammate>();
                    List<PlayerGameOpponent> opponentList = new List<PlayerGameOpponent>();

                    var totalGameTeammates = await _context.PlayerRoundTeammates.Where(w => w.FkIdPlayer == player.FkIdPlayer && w.FkIdGame == match.IdGame).Select(s => s.FkIdTeammate).Distinct().ToListAsync(cancellationToken);
                    var totalGameOpponents = await _context.PlayerRoundOpponents.Where(w => w.FkIdPlayer == player.FkIdPlayer && w.FkIdGame == match.IdGame).Select(s => s.FkIdOpponent).Distinct().ToListAsync(cancellationToken);

                    foreach (var teammate in totalGameTeammates)
                    {
                        teammateList.Add(new PlayerGameTeammate
                        {
                            FkIdGame = match.IdGame,
                            FkIdPlayer = player.FkIdPlayer,
                            FkIdPlayerGameRecordNavigation = gameRecord,
                            FkIdSeason = match.FkIdSeason,
                            FkIdTeam = player.FkIdTeam,
                            FkIdTeammate = teammate,
                            FkIdWeek = match.FkIdWeek
                        });
                    }

                    foreach (var opponent in totalGameOpponents)
                    {
                        opponentList.Add(new PlayerGameOpponent
                        {
                            FkIdGame = match.IdGame,
                            FkIdPlayer = player.FkIdPlayer,
                            FkIdPlayerGameRecordNavigation = gameRecord,
                            FkIdSeason = match.FkIdSeason,
                            FkIdTeam = player.FkIdTeam,
                            FkIdOpponent = opponent,
                            FkIdWeek = match.FkIdWeek,
                        });
                    }

                    gamePlayerList.Add(gamePlayer);
                    playerGameRecordList.Add(gameRecord);

                    _context.PlayerGameOpponents.AddRange(opponentList);
                    _context.PlayerGameTeammates.AddRange(teammateList);
                }

                _context.GamePlayers.AddRange(gamePlayerList);
                _context.PlayerGameRecords.AddRange(playerGameRecordList);

                // Create game team stats.

                var redTeamStats = request.FlipTeams ? rounds.Select(s => s.BlueTeamStats) : rounds.Select(s => s.RedTeamStats);
                var blueTeamStats = request.FlipTeams ? rounds.Select(s => s.RedTeamStats) : rounds.Select(s => s.BlueTeamStats);
                var redPlayers = request.FlipTeams ? rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Blue) : rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Red);
                var bluePlayers = request.FlipTeams ? rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Red) : rounds.SelectMany(s => s.PlayerStats).Where(w => w.Team == LogFileEnums.Teams.Blue);

                var gameTeamStatsRed = GameHelper.CalculateGameTeamStats(
                    match.IdGame,
                    match.FkIdSeason,
                    match.FkIdWeek,
                    match.FkIdTeamRed,
                    match.FkIdTeamBlue,
                    (uint)rounds.Count,
                    (uint)rounds.Sum(s => s.MetaData.DurationTics),
                    redTeamStats,
                    redPlayers,
                    (uint)blueTeamStats.Sum(s => s.Points),
                    LogFileEnums.Teams.Red,
                    gameWinner);

                var gameTeamStatsBlue = GameHelper.CalculateGameTeamStats(
                    match.IdGame,
                    match.FkIdSeason,
                    match.FkIdWeek,
                    match.FkIdTeamBlue,
                    match.FkIdTeamRed,
                    (uint)rounds.Count,
                    (uint)rounds.Sum(s => s.MetaData.DurationTics),
                    blueTeamStats,
                    bluePlayers,
                    (uint)redTeamStats.Sum(s => s.Points),
                    LogFileEnums.Teams.Blue,
                    gameWinner);

                _context.GameTeamStats.Add(gameTeamStatsBlue);
                _context.GameTeamStats.Add(gameTeamStatsRed);

                uint? winningTeam = null;
                var winningColor = "t";

                if (gameWinner.GameResult == LogFileEnums.GameResult.RedWin)
                {
                    winningTeam = match.FkIdTeamRed;
                    winningColor = "r";
                }
                else if (gameWinner.GameResult == LogFileEnums.GameResult.BlueWin)
                {
                    winningTeam = match.FkIdTeamBlue;
                    winningColor = "b";
                }

                match.TeamWinnerColor = winningColor;
                match.FkIdTeamWinner = winningTeam;

                await _context.SaveChangesAsync(cancellationToken);

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
