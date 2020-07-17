using System;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class MatchStatsDto
    {
        public TimeSpan TimePlayed { get; set; }
        public int Captures { get; set; }
        public int PickupCaptures { get; set; }
        public int Assists { get; set; }
        public int? DamageBetweenTouchAndCaptureMax { get; set; }
        public double? DamageBetweenTouchAndCaptureAverage { get; set; }
        public TimeSpan CaptureTimeMin { get; set; }
        public TimeSpan CaptureTimeMax { get; set; }
        public TimeSpan CaptureTimeAverage { get; set; }
        public int? CaptureHealthMin { get; set; }
        public int? CaptureHealthMax { get; set; }
        public double? CaptureHealthAverage { get; set; }
        public int? CaptureGreenArmorMin { get; set; }
        public int? CaptureGreenArmorMax { get; set; }
        public double? CaptureGreenArmorAverage { get; set; }
        public int? CaptureBlueArmorMin { get; set; }
        public int? CaptureBlueArmorMax { get; set; }
        public double? CaptureBlueArmorAverage { get; set; }
        public int CapturesWithSuperPickups { get; set; }
        public int FlagCarriersKilledWhileHoldingFlag { get; set; }
        public int HighestKillsBeforeCapturing { get; set; }
        public TimeSpan PickupCaptureTimeMin { get; set; }
        public TimeSpan PickupCaptureTimeMax { get; set; }
        public TimeSpan PickupCaptureTimeAverage { get; set; }
        public int FlagTouches { get; set; }
        public int PickupFlagTouches { get; set; }
        public int FlagDefenses { get; set; }
        public int FlagReturns { get; set; }
        public int Frags { get; set; }
        public int Deaths { get; set; }
        public int EnvironmentalDeaths { get; set; }
        public int EnvironmentalFlagCarrierDeaths { get; set; }
        public int TeamKills { get; set; }
        public int KillingSprees { get; set; }
        public int Rampages { get; set; }
        public int Dominatings { get; set; }
        public int Unstoppables { get; set; }
        public int GodLikes { get; set; }
        public int WickedSicks { get; set; }
        public int LongestSpree { get; set; }
        public int DoubleKills { get; set; }
        public int MultiKills { get; set; }
        public int UltraKills { get; set; }
        public int MonsterKills { get; set; }
        public int HighestMultiKill { get; set; }
        public int PickupHealthGained { get; set; }
        public int HealthFromNonPowerups { get; set; }
        public int? HealthWhenTouchingFlagMin { get; set; }
        public int? HealthWhenTouchingFlagMax { get; set; }
        public double? HealthWhenTouchingFlagAverage { get; set; }
        public int? GreenArmorWhenTouchingFlagMin { get; set; }
        public int? GreenArmorWhenTouchingFlagMax { get; set; }
        public double? GreenArmorWhenTouchingFlagAverage { get; set; }
        public int? BlueArmorWhenTouchingFlagMin { get; set; }
        public int? BlueArmorWhenTouchingFlagMax { get; set; }
        public double? BlueArmorWhenTouchingFlagAverage { get; set; }
        public int? HealthWhenTouchingFlagThatResultsInCaptureMin { get; set; }
        public int? HealthWhenTouchingFlagThatResultsInCaptureMax { get; set; }
        public double? HealthWhenTouchingFlagThatResultsInCaptureAverage { get; set; }
        public int Damage { get; set; }
        public int DamageTakenFromEnvironment { get; set; }
        public int DamageTakenFromEnvironmentAsFlagCarrier { get; set; }
        public int DamageToFlagCarriers { get; set; }
        public int DamageDoneWithFlag { get; set; }
        public int DamageToGreenArmor { get; set; }
        public int DamageToBlueArmor { get; set; }
        public int Powerups { get; set; }
    }
}
