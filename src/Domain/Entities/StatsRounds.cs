
namespace WorldDoomLeague.Domain.Entities
{
    public partial class StatsRounds
    {
        public uint IdStatsRound { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdMap { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdTeam { get; set; }
        public string Team { get; set; }
        public int AccuracyCompleteHits { get; set; }
        public int AccuracyCompleteMisses { get; set; }
        public int TotalAssists { get; set; }
        public int TotalCaptures { get; set; }
        public int? DamageOutputBetweenTouchCaptureMax { get; set; }
        public int? DamageOutputBetweenTouchCaptureAverage { get; set; }
        public int? CaptureTicsMin { get; set; }
        public int? CaptureTicsMax { get; set; }
        public double? CaptureTicsAverage { get; set; }
        public int? CaptureHealthMin { get; set; }
        public int? CaptureHealthMax { get; set; }
        public double? CaptureHealthAverage { get; set; }
        public int? CaptureGreenArmorMin { get; set; }
        public int? CaptureGreenArmorMax { get; set; }
        public double? CaptureGreenArmorAverage { get; set; }
        public int? CaptureBlueArmorMin { get; set; }
        public int? CaptureBlueArmorMax { get; set; }
        public double? CaptureBlueArmorAverage { get; set; }
        public int CaptureWithSuperPickups { get; set; }
        public int CarriersKilledWhileHoldingFlag { get; set; }
        public int HighestKillsBeforeCapturing { get; set; }
        public int TotalPickupCaptures { get; set; }
        public int? PickupCaptureTicsMin { get; set; }
        public int? PickupCaptureTicsMax { get; set; }
        public double? PickupCaptureTicsAverage { get; set; }
        public int TotalDamage { get; set; }
        public int TotalDamageGreenArmor { get; set; }
        public int TotalDamageBlueArmor { get; set; }
        public int TotalDamageFlagCarrier { get; set; }
        public int TotalDamageTakenEnvironment { get; set; }
        public int TotalDamageCarrierTakenEnvironment { get; set; }
        public int TotalDamageWithFlag { get; set; }
        public int TotalFlagReturns { get; set; }
        public int TotalKills { get; set; }
        public int TotalCarrierKills { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalEnvironmentDeaths { get; set; }
        public int TotalEnvironmentCarrierDeaths { get; set; }
        public int AmountTeamKills { get; set; }
        public int SpreeKillingSprees { get; set; }
        public int SpreeRampage { get; set; }
        public int SpreeDominations { get; set; }
        public int SpreeUnstoppables { get; set; }
        public int SpreeGodlikes { get; set; }
        public int SpreeWickedsicks { get; set; }
        public int MultiDoubleKills { get; set; }
        public int MultiMultiKills { get; set; }
        public int MultiUltraKills { get; set; }
        public int MultiMonsterKills { get; set; }
        public int LongestSpree { get; set; }
        public int HighestMultiFrags { get; set; }
        public int TotalPowerPickups { get; set; }
        public int PickupHealthGained { get; set; }
        public int HealthFromNonpowerPickups { get; set; }
        public int TotalTouches { get; set; }
        public int TotalPickupTouches { get; set; }
        public int? TouchHealthMin { get; set; }
        public int? TouchHealthMax { get; set; }
        public double? TouchHealthAverage { get; set; }
        public int? TouchGreenArmorMin { get; set; }
        public int? TouchGreenArmorMax { get; set; }
        public double? TouchGreenArmorAverage { get; set; }
        public int? TouchBlueArmorMin { get; set; }
        public int? TouchBlueArmorMax { get; set; }
        public double? TouchBlueArmorAverage { get; set; }
        public int? TouchHealthResultCaptureMin { get; set; }
        public int? TouchHealthResultCaptureMax { get; set; }
        public double? TouchHealthResultCaptureAverage { get; set; }
        public int TouchesWithOverHundredHealth { get; set; }
        public int EfficiencyPoints { get; set; }

        public virtual Games FkIdGameNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual Teams FkIdTeamNavigation { get; set; }
    }
}
