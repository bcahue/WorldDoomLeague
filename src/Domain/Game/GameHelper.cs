using System.Collections.Generic;
using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Domain.MatchModel;
using WorldDoomLeague.Domain.Enums;

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

        public static StatsRounds CreateStatRound(PlayerStats playerStats, uint matchId, uint mapId, uint seasonId, uint weekId, uint teamId, uint playerId, uint roundId, LogFileEnums.Teams team)
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
    }
}
