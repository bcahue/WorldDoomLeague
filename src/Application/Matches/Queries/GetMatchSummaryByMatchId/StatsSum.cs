using System;
using System.Collections.Generic;
using System.Linq;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public static class Score
    {
        public static MatchStatsDto SumStats (IEnumerable<StatsRounds> statsRounds)
        {

            return new MatchStatsDto
            {
                // Assists simply track players who were apart of a flag capture (who didn't actually capture it.)
                Assists = statsRounds.Sum(s => s.TotalAssists),
                // Captures and stats relating to captures.
                Captures = statsRounds.Sum(s => s.TotalCaptures),
                PickupCaptures = statsRounds.Sum(s => s.TotalPickupCaptures),
                CaptureBlueArmorAverage = statsRounds.Where(w => w.CaptureBlueArmorAverage > 0).Select(s => s.CaptureBlueArmorAverage).DefaultIfEmpty().Average(),
                CaptureBlueArmorMax = statsRounds.Where(w => w.CaptureBlueArmorMax > 0).Select(w => w.CaptureBlueArmorMax).DefaultIfEmpty().Max(),
                CaptureBlueArmorMin = statsRounds.Where(w => w.CaptureBlueArmorMin > 0).Select(w => w.CaptureBlueArmorMin).DefaultIfEmpty().Min(),
                CaptureGreenArmorAverage = statsRounds.Where(w => w.CaptureGreenArmorAverage > 0).Select(w => w.CaptureGreenArmorAverage).DefaultIfEmpty().Average(),
                CaptureGreenArmorMax = statsRounds.Where(w => w.CaptureGreenArmorMax > 0).Select(w => w.CaptureGreenArmorMax).DefaultIfEmpty().Max(),
                CaptureGreenArmorMin = statsRounds.Where(w => w.CaptureGreenArmorMin > 0).Select(w => w.CaptureGreenArmorMin).DefaultIfEmpty().Min(),
                CaptureHealthAverage = statsRounds.Where(w => w.CaptureHealthAverage > 0).Select(w => w.CaptureHealthAverage).DefaultIfEmpty().Average(),
                CaptureHealthMax = statsRounds.Where(w => w.CaptureHealthMax > 0).Select(w => w.CaptureHealthMax).DefaultIfEmpty().Max(),
                CaptureHealthMin = statsRounds.Where(w => w.CaptureHealthMin > 0).Select(w => w.CaptureHealthMin).DefaultIfEmpty().Min(),
                CapturesWithSuperPickups = statsRounds.Sum(s => s.CaptureWithSuperPickups),
                HighestKillsBeforeCapturing = statsRounds.Sum(s => s.HighestKillsBeforeCapturing),
                CaptureTimeAverage = TimeSpan.FromSeconds((statsRounds.Select(w => w.CaptureTicsAverage).DefaultIfEmpty().Average() ?? 0.0) / 35),
                CaptureTimeMin = TimeSpan.FromSeconds((statsRounds.Where(w => w.CaptureTicsMin > 0).Select(w => w.CaptureTicsMin).DefaultIfEmpty().Min() ?? 0.0) / 35),
                CaptureTimeMax = TimeSpan.FromSeconds((statsRounds.Where(w => w.CaptureTicsMax > 0).Select(w => w.CaptureTicsMax).DefaultIfEmpty().Max() ?? 0.0) / 35),
                PickupCaptureTimeAverage = TimeSpan.FromSeconds((statsRounds.Where(w => w.PickupCaptureTicsAverage > 0).Select(w => w.PickupCaptureTicsAverage).DefaultIfEmpty().Average() ?? 0.0) / 35),
                PickupCaptureTimeMin = TimeSpan.FromSeconds((statsRounds.Where(w => w.PickupCaptureTicsMin > 0).Select(w => w.PickupCaptureTicsMin).DefaultIfEmpty().Min() ?? 0.0) / 35),
                PickupCaptureTimeMax = TimeSpan.FromSeconds((statsRounds.Where(w => w.PickupCaptureTicsMax > 0).Select(w => w.PickupCaptureTicsMax).DefaultIfEmpty().Max() ?? 0.0) / 35),
                // Damage and stats related to damage.
                Damage = statsRounds.Sum(s => (s.TotalDamage + s.TotalDamageFlagCarrier)),
                DamageBetweenTouchAndCaptureAverage = statsRounds.Select(w => w.DamageOutputBetweenTouchCaptureAverage).DefaultIfEmpty().Average(),
                DamageBetweenTouchAndCaptureMax = statsRounds.Select(w => w.DamageOutputBetweenTouchCaptureMax).DefaultIfEmpty().Max(),
                //DamageBetweenTouchAndCaptureMin         = statsRounds.Select(w => w.DamageOutputBetweenTouchCaptureMin > 0)
                //                                            .Max(s => s.DamageOutputBetweenTouchCaptureMin),
                DamageDoneWithFlag = statsRounds.Sum(s => s.TotalDamageWithFlag),
                DamageTakenFromEnvironment = statsRounds.Sum(s => s.TotalDamageTakenEnvironment),
                DamageTakenFromEnvironmentAsFlagCarrier = statsRounds.Sum(s => s.TotalDamageCarrierTakenEnvironment),
                DamageToBlueArmor = statsRounds.Sum(s => s.TotalDamageBlueArmor),
                DamageToFlagCarriers = statsRounds.Sum(s => s.TotalDamageFlagCarrier),
                DamageToGreenArmor = statsRounds.Sum(s => s.TotalDamageGreenArmor),
                // Deaths and stats related to deaths
                Deaths = statsRounds.Sum(s => s.TotalDeaths),
                EnvironmentalDeaths = statsRounds.Sum(s => s.TotalEnvironmentDeaths),
                EnvironmentalFlagCarrierDeaths = statsRounds.Sum(s => s.TotalEnvironmentCarrierDeaths),
                // Flag defenses and related stats.
                FlagDefenses = statsRounds.Sum(s => s.TotalCarrierKills),
                // Flag returns and related stats.
                FlagReturns = statsRounds.Sum(s => s.TotalFlagReturns),
                // Flag touches and related stats.
                FlagTouches = statsRounds.Sum(s => s.TotalTouches),
                BlueArmorWhenTouchingFlagAverage = statsRounds.Where(w => w.TouchBlueArmorAverage > 0).Select(w => w.TouchBlueArmorAverage).DefaultIfEmpty().Average(),
                BlueArmorWhenTouchingFlagMax = statsRounds.Where(w => w.TouchBlueArmorMax > 0).Select(w => w.TouchBlueArmorMax).DefaultIfEmpty().Max(),
                BlueArmorWhenTouchingFlagMin = statsRounds.Where(w => w.TouchBlueArmorMin > 0).Select(w => w.TouchBlueArmorMin).DefaultIfEmpty().Min(),
                GreenArmorWhenTouchingFlagAverage = statsRounds.Where(w => w.TouchGreenArmorAverage > 0).Select(w => w.TouchGreenArmorAverage).DefaultIfEmpty().Average(),
                GreenArmorWhenTouchingFlagMax = statsRounds.Where(w => w.TouchGreenArmorMax > 0).Select(w => w.TouchGreenArmorMax).DefaultIfEmpty().Max(),
                GreenArmorWhenTouchingFlagMin = statsRounds.Where(w => w.TouchGreenArmorMin > 0).Select(w => w.TouchGreenArmorMin).DefaultIfEmpty().Min(),
                HealthWhenTouchingFlagAverage = statsRounds.Where(w => w.TouchHealthAverage > 0).Select(w => w.TouchHealthAverage).DefaultIfEmpty().Average(),
                HealthWhenTouchingFlagMax = statsRounds.Where(w => w.TouchHealthMax > 0).Select(w => w.TouchHealthMax).DefaultIfEmpty().Max(),
                HealthWhenTouchingFlagMin = statsRounds.Where(w => w.TouchHealthMin > 0).Select(w => w.TouchHealthMin).DefaultIfEmpty().Min(),
                HealthWhenTouchingFlagThatResultsInCaptureAverage = statsRounds.Where(w => w.TouchHealthResultCaptureAverage > 0).Select(w => w.TouchHealthResultCaptureAverage).DefaultIfEmpty().Average(),
                HealthWhenTouchingFlagThatResultsInCaptureMax = statsRounds.Where(w => w.TouchHealthResultCaptureMax > 0).Select(w => w.TouchHealthResultCaptureMax).DefaultIfEmpty().Max(),
                HealthWhenTouchingFlagThatResultsInCaptureMin = statsRounds.Where(w => w.TouchHealthResultCaptureMin > 0).Select(w => w.TouchHealthResultCaptureMin).DefaultIfEmpty().Min(),
                PickupFlagTouches = statsRounds.Sum(s => s.TotalPickupTouches),
                // Frags and frag stats
                Frags = statsRounds.Sum(s => (s.TotalKills + s.TotalCarrierKills)),
                FlagCarriersKilledWhileHoldingFlag = statsRounds.Sum(s => s.CarriersKilledWhileHoldingFlag),
                // Pickups
                Powerups = statsRounds.Sum(s => s.TotalPowerPickups),
                PickupHealthGained = statsRounds.Sum(s => s.PickupHealthGained),
                HealthFromNonPowerups = statsRounds.Sum(s => s.HealthFromNonpowerPickups),
                // Sprees
                LongestSpree = statsRounds.Where(w => w.LongestSpree > 0).Select(w => w.LongestSpree).DefaultIfEmpty().Max(),
                // Multi Kills
                HighestMultiKill = statsRounds.Where(w => w.HighestMultiFrags > 0).Select(w => w.HighestMultiFrags).DefaultIfEmpty().Max(),
                // Metadata (used to find per minute stat modes, like per minute or per 8 or per 24 stats etc.)
                TimePlayed = TimeSpan.FromSeconds((double)statsRounds.Select(s => s.FkIdRoundNavigation).Distinct().Sum(s => s.RoundTicsDuration) / 35)
            };
        }
    }
}
