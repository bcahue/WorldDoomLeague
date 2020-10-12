using System.Collections.Generic;
using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Domain.MatchModel;
using WorldDoomLeague.Domain.Enums;
using System;
using System.Linq;

namespace WorldDoomLeague.Domain.Game
{
    public static class GameHelper
    {
        public static RoundResults CalculateRoundResult (int redPoints, int bluePoints, IList<uint> redTeamIds, IList<uint> blueTeamIds)
        {
            if (redPoints > bluePoints)
            {
                return new RoundResults
                {
                    BlueRoundPlayerIds = blueTeamIds,
                    RedRoundPlayerIds = redTeamIds,
                    RoundResult = LogFileEnums.GameResult.RedWin
                };
            }
            else if (redPoints < bluePoints)
            {
                return new RoundResults
                {
                    BlueRoundPlayerIds = blueTeamIds,
                    RedRoundPlayerIds = redTeamIds,
                    RoundResult = LogFileEnums.GameResult.BlueWin
                };
            }
            else
            {
                return new RoundResults
                {
                    BlueRoundPlayerIds = blueTeamIds,
                    RedRoundPlayerIds = redTeamIds,
                    RoundResult = LogFileEnums.GameResult.TieGame
                };
            }
        }

        public static StatsRounds CreateStatRound(PlayerStats playerStats,
            uint matchId,
            uint mapId,
            uint seasonId,
            uint weekId,
            uint teamId,
            uint playerId,
            uint roundId,
            LogFileEnums.Teams team)
        {
            string s;
            if (team == LogFileEnums.Teams.Blue) { s = "b"; } else if (team == LogFileEnums.Teams.Red) { s = "r"; } else { s = "n"; }

            return new StatsRounds
            {
                // Foreign Keys
                FkIdGame = matchId,
                FkIdMap = mapId,
                FkIdSeason = seasonId,
                FkIdWeek = weekId,
                FkIdTeam = teamId,
                FkIdPlayer = playerId,
                FkIdRound = roundId,
                // Team
                Team = s,
                // Accuracy
                AccuracyCompleteHits = playerStats.CompleteHits,
                AccuracyCompleteMisses = playerStats.CompleteMisses,
                // oopsies
                AmountTeamKills = playerStats.TeamKills,
                TotalSuicides = playerStats.Suicides,
                TotalSuicidesWithFlag = playerStats.SuicidesWithFlag,
                // Min/Max/Avgs
                CaptureBlueArmorAverage = playerStats.CaptureBlueArmorAvg,
                CaptureBlueArmorMax = playerStats.CaptureBlueArmorMax,
                CaptureBlueArmorMin = playerStats.CaptureBlueArmorMin,
                CaptureGreenArmorAverage = playerStats.CaptureGreenArmorAvg,
                CaptureGreenArmorMax = playerStats.CaptureGreenArmorMax,
                CaptureGreenArmorMin = playerStats.CaptureGreenArmorMin,
                CaptureHealthAverage = playerStats.CaptureHealthAvg,
                CaptureHealthMax = playerStats.CaptureHealthMax,
                CaptureHealthMin = playerStats.CaptureHealthMin,
                CaptureTicsAverage = playerStats.CaptureTimeAvgTics,
                CaptureTicsMax = playerStats.CaptureTimeMaxTics,
                CaptureTicsMin = playerStats.CaptureTimeMinTics,
                PickupCaptureTicsAverage = playerStats.PickupCaptureTimeAvgTics,
                PickupCaptureTicsMax = playerStats.PickupCaptureTimeMaxTics,
                PickupCaptureTicsMin = playerStats.PickupCaptureTimeMinTics,
                TouchBlueArmorAverage = playerStats.TouchBlueArmorAvg,
                TouchBlueArmorMax = playerStats.TouchBlueArmorMax,
                TouchBlueArmorMin = playerStats.TouchBlueArmorMin,
                TouchHealthResultCaptureAverage = playerStats.TouchHealthResultCaptureAvg,
                TouchHealthResultCaptureMax = playerStats.TouchHealthResultCaptureMax,
                TouchHealthResultCaptureMin = playerStats.TouchHealthResultCaptureMin,
                DamageOutputBetweenTouchCaptureAverage = (int)playerStats.DamageOutputBetweenTouchAndCaptureAvg,
                DamageOutputBetweenTouchCaptureMax = playerStats.DamageOutputBetweenTouchAndCaptureMax,
                DamageOutputBetweenTouchCaptureMin = playerStats.DamageOutputBetweenTouchAndCaptureMin,
                TouchGreenArmorAverage = playerStats.TouchGreenArmorAvg,
                TouchGreenArmorMax = playerStats.TouchGreenArmorMax,
                TouchGreenArmorMin = playerStats.TouchGreenArmorMin,
                TouchHealthAverage = playerStats.TouchHealthAvg,
                TouchHealthMax = playerStats.TouchHealthMax,
                TouchHealthMin = playerStats.TouchHealthMin,
                // Frags
                CarriersKilledWhileHoldingFlag = playerStats.FlagCarriersKilledWhileHoldingFlag,
                TotalKills = playerStats.TotalKills,
                HighestMultiFrags = playerStats.HighestMultiKill,
                LongestSpree = playerStats.LongestSpree,
                TotalCarrierKills = playerStats.FlagDefenses,
                HighestKillsBeforeCapturing = playerStats.HighestKillsBeforeCapturing,
                // Damage
                TotalDamage = playerStats.TotalDamage,
                TotalDamageBlueArmor = playerStats.TotalBlueArmorDamage,
                TotalDamageGreenArmor = playerStats.TotalGreenArmorDamage,
                TotalDamageFlagCarrier = playerStats.TotalDamageToFlagCarriers,
                TotalDamageToFlagCarriersWhileHoldingFlag = playerStats.TotalDamageToFlagCarriersWhileHoldingFlag,
                TotalDamageWithFlag = playerStats.TotalDamageAsFlagCarrier,
                TotalDamageTakenEnvironment = playerStats.TotalDamageTakenFromEnvironment,
                TotalDamageCarrierTakenEnvironment = playerStats.TotalDamageTakenFromEnvironmentAsFlagCarrier,
                // Flags
                CaptureWithSuperPickups = playerStats.CapturesWithSuperPickup,
                TotalCaptures = playerStats.Captures,
                TotalPickupCaptures = playerStats.PickupCaptures,
                TotalFlagReturns = playerStats.TotalFlagReturns,
                TotalAssists = playerStats.Assists,
                // Touches
                TouchesWithOverHundredHealth = playerStats.TouchesOverOneHundredHealth,
                TotalPickupTouches = playerStats.PickupFlagTouches,
                TotalTouches = playerStats.FlagTouches,
                // Pickups
                HealthFromNonpowerPickups = playerStats.HealthFromNonPowerPickups,
                PickupHealthGained = playerStats.TotalPowerPickups,
                TotalPowerPickups = playerStats.TotalPowerPickups,
                // Deaths
                TotalDeaths = playerStats.Deaths,
                TotalEnvironmentCarrierDeaths = playerStats.EnvironmentalDeathsAsFlagCarrier,
                TotalEnvironmentDeaths = playerStats.EnvironmentalDeaths,
            };
        }

        public static IEnumerable<StatsAccuracyData> GetAccuracyEntities (IList<AccuracyAggregate> accuracyAggregates, uint attackerId, uint roundId)
        {
            foreach (var ev in accuracyAggregates)
            {
                var accuracy = new StatsAccuracyData
                {
                    FkIdPlayerAttacker = attackerId,
                    FkIdRound = roundId,
                    HitMissRatio = ev.HitMissRatio,
                    WeaponType = (byte)ev.Weapon,
                    PinpointPercent = ev.PinpointPercentage,
                    SpritePercent = ev.SpritePercentage
                };

                yield return accuracy;
            }
        }

        public static IEnumerable<StatsAccuracyWithFlagData> GetAccuracyWithFlagEntities(IList<AccuracyAggregate> accuracyAggregates, uint attackerId, uint roundId)
        {
            foreach (var ev in accuracyAggregates)
            {
                var accuracyWithFlag = new StatsAccuracyWithFlagData
                {
                    FkIdPlayerAttacker = attackerId,
                    FkIdRound = roundId,
                    HitMissRatio = ev.HitMissRatio,
                    WeaponType = (byte)ev.Weapon,
                    PinpointPercent = ev.PinpointPercentage,
                    SpritePercent = ev.SpritePercentage
                };

                yield return accuracyWithFlag;
            }
        }

        public static IEnumerable<StatsDamageData> GetDamageEntities(IList<DamageAggregate> damageAggregates, uint attackerId, IDictionary<string, uint> targetPairs, uint roundId)
        {
            foreach (var ev in damageAggregates)
            {
                var damage = new StatsDamageData
                {
                    FkIdRound = roundId,
                    FkIdPlayerAttacker = attackerId,
                    FkIdPlayerTarget = targetPairs[ev.TargetName],
                    DamageBlueArmor = (uint)ev.TotalDamageBlueArmor,
                    DamageGreenArmor = (uint)ev.TotalDamageGreenArmor,
                    DamageHealth = (uint)ev.TotalDamage,
                    WeaponType = (byte)ev.Weapon
                };

                yield return damage;
            }
        }

        public static IEnumerable<StatsDamageWithFlagData> GetDamageWithFlagEntities(IList<DamageAggregate> damageAggregates, uint attackerId, IDictionary<string, uint> targetPairs, uint roundId)
        {
            foreach (var ev in damageAggregates)
            {
                var damageWithFlag = new StatsDamageWithFlagData
                {
                    FkIdRound = roundId,
                    FkIdPlayerAttacker = attackerId,
                    FkIdPlayerTarget = targetPairs[ev.TargetName],
                    DamageBlueArmor = (uint)ev.TotalDamageBlueArmor,
                    DamageGreenArmor = (uint)ev.TotalDamageGreenArmor,
                    DamageHealth = (uint)ev.TotalDamage,
                    WeaponType = (byte)ev.Weapon
                };

                yield return damageWithFlag;
            }
        }

        public static IEnumerable<StatsKillData> GetKillsEntities(IList<KillAggregate> killAggregates, uint attackerId, IDictionary<string, uint> targetPairs, uint roundId)
        {
            foreach (var ev in killAggregates)
            {
                var kills = new StatsKillData
                {
                    FkIdPlayerAttacker = attackerId,
                    FkIdPlayerTarget = targetPairs[ev.TargetName],
                    FkIdRound = roundId,
                    WeaponType = (byte)ev.Weapon,
                    TotalKills = (byte)ev.TotalKills
                };

                yield return kills;
            }
        }

        public static IEnumerable<StatsKillCarrierData> GetKillsWithFlagEntities(IList<KillAggregate> killAggregates, uint attackerId, IDictionary<string, uint> targetPairs, uint roundId)
        {
            foreach (var ev in killAggregates)
            {
                var killsWithFlag = new StatsKillCarrierData
                {
                    FkIdPlayerAttacker = attackerId,
                    FkIdPlayerTarget = targetPairs[ev.TargetName],
                    FkIdRound = roundId,
                    WeaponType = (byte)ev.Weapon,
                    TotalKills = (byte)ev.TotalKills
                };

                yield return killsWithFlag;
            }
        }

        public static IEnumerable<StatsPickupData> GetPickupsEntities(IList<PickupAggregate> pickupAggregates, uint activatorId, uint roundId)
        {
            foreach (var ev in pickupAggregates)
            {
                var pickups = new StatsPickupData
                {
                    FkIdRound = roundId,
                    FkIdActivatorPlayer = activatorId,
                    PickupType = (byte)ev.PickupType,
                    PickupAmount = (uint)ev.TotalPickups

                };

                yield return pickups;
            }
        }

        public static uint CalculatePoints(int roundWins, int opponentRoundWins)
        {
            if (roundWins > opponentRoundWins && opponentRoundWins == 0)
            {
                return 4;
            }
            else if (roundWins > opponentRoundWins && opponentRoundWins > 0)
            {
                return 3;
            }
            else if (roundWins == opponentRoundWins)
            {
                return 2;
            }
            else if (roundWins < opponentRoundWins && roundWins > 0)
            {
                return 1;
            }
            else if (roundWins < opponentRoundWins && roundWins == 0)
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public static GameTeamStats CalculateGameTeamStats(
            uint gameId,
            uint seasonId,
            uint weekId,
            uint teamId,
            uint opponentTeamId,
            uint numRoundsPlayed,
            uint numTicsTotal,
            IEnumerable<TeamStats> teamStats,
            IEnumerable<PlayerStats> playerStats,
            uint pointsAgainst,
            LogFileEnums.Teams team,
            GameRecordKeeper gameRecord
            )
        {
            string teamColor = "";
            uint win = 0;
            uint loss = 0;
            uint tie = 0;
            uint points = 0;


            if (team == LogFileEnums.Teams.Blue)
            {
                teamColor = "b";
                win = gameRecord.GameResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0;
                loss = gameRecord.GameResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0;
                tie = gameRecord.GameResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0;
                points = CalculatePoints(
                    gameRecord.RoundResults.Where(w => w.RoundResult == LogFileEnums.GameResult.BlueWin).Count(),
                    gameRecord.RoundResults.Where(w => w.RoundResult == LogFileEnums.GameResult.RedWin).Count()
                    );
            }
            else if (team == LogFileEnums.Teams.Red)
            { 
                teamColor = "r";
                win = gameRecord.GameResult == LogFileEnums.GameResult.RedWin ? (uint)1 : (uint)0;
                loss = gameRecord.GameResult == LogFileEnums.GameResult.BlueWin ? (uint)1 : (uint)0;
                tie = gameRecord.GameResult == LogFileEnums.GameResult.TieGame ? (uint)1 : (uint)0;
                points = CalculatePoints(
                    gameRecord.RoundResults.Where(w => w.RoundResult == LogFileEnums.GameResult.RedWin).Count(),
                    gameRecord.RoundResults.Where(w => w.RoundResult == LogFileEnums.GameResult.BlueWin).Count()
                    );
            }

            return new GameTeamStats
            {
                FkIdGame = gameId,
                FkIdSeason = seasonId,
                FkIdWeek = weekId,
                FkIdTeam = teamId,
                FkIdOpponentTeam = opponentTeamId,
                NumberRoundsPlayed = numRoundsPlayed,
                NumberTicsPlayed = numTicsTotal,
                CapturesFor = (uint)teamStats.Sum(s => s.Points),
                CapturesAgainst = pointsAgainst,
                HighestMultiKill = (uint)playerStats.DefaultIfEmpty().Max(m => m.HighestMultiKill),
                LongestSpree = (uint)playerStats.DefaultIfEmpty().Max(m => m.LongestSpree),
                TeamColor = teamColor,
                TotalAssists = (uint)teamStats.Sum(s => s.Assists),
                TotalCaptures = (uint)teamStats.Sum(s => s.Captures),
                TotalCarrierDamage = (uint)playerStats.Sum(s => s.TotalDamageToFlagCarriers),
                TotalCarrierKills = (uint)teamStats.Sum(s => s.FlagDefenses),
                TotalDamage = (uint)playerStats.Sum(s => s.TotalDamage),
                TotalDamageWithFlag = (uint)playerStats.Sum(s => s.TotalDamageAsFlagCarrier),
                TotalDeaths = (uint)playerStats.Sum(s => s.TotalDeaths),
                TotalEnvironmentDeaths = (uint)playerStats.Sum(s => s.EnvironmentalDeaths + s.EnvironmentalDeathsAsFlagCarrier),
                TotalFlagReturns = (uint)playerStats.Sum(s => s.TotalFlagReturns),
                TotalPowerPickups = (uint)playerStats.Sum(s => s.TotalPowerPickups),
                TotalKills = (uint)playerStats.Sum(s => s.TotalKills),
                TotalPickupTouches = (uint)playerStats.Sum(s => s.PickupFlagTouches),
                TotalTouches = (uint)playerStats.Sum(s => s.FlagTouches),
                TotalPickupCaptures = (uint)playerStats.Sum(s => s.CapturesWithSuperPickup),
                Win = win,
                Tie = tie,
                Loss = loss,
                Points = points
            };
        }
    }
}
