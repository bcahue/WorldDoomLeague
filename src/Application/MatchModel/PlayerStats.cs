using System;
using System.Collections.Generic;
using System.Linq;
using WorldDoomLeague.Domain.Enums;
using WorldDoomLeague.Application.MatchModel;

namespace WorldDoomLeague.Application.MatchModel
{
    public class PlayerStats
    {
        public string Name { get; set; }
        public LogFileEnums.Teams Team { get; set; }
        public int CompleteHits { get; set; }
        public int CompleteMisses { get; set; }
        public int Assists { get; set; }
        public int Captures { get; set; }
        public int PickupCaptures { get; set; }
        public int FlagTouches { get; set; }
        public int PickupFlagTouches { get; set; }
        public int DamageOutputBetweenTouchAndCaptureMin { get; set; }
        public int DamageOutputBetweenTouchAndCaptureMax { get; set; }
        public double DamageOutputBetweenTouchAndCaptureAvg { get; set; }
        public TimeSpan CaptureTimeMin { get; set; }
        public TimeSpan CaptureTimeMax { get; set; }
        public TimeSpan CaptureTimeAvg { get; set; }
        public int CaptureTimeMinTics { get; set; }
        public int CaptureTimeMaxTics { get; set; }
        public double CaptureTimeAvgTics { get; set; }
        public int CaptureHealthMin { get; set; }
        public int CaptureHealthMax { get; set; }
        public double CaptureHealthAvg { get; set; }
        public int CaptureGreenArmorMin { get; set; }
        public int CaptureGreenArmorMax { get; set; }
        public double CaptureGreenArmorAvg { get; set; }
        public int CaptureBlueArmorMin { get; set; }
        public int CaptureBlueArmorMax { get; set; }
        public double CaptureBlueArmorAvg { get; set; }
        public int FlagCarriersKilledWhileHoldingFlag { get; set; }
        public int HighestKillsBeforeCapturing { get; set; }
        public TimeSpan PickupCaptureTimeMin { get; set; }
        public TimeSpan PickupCaptureTimeMax { get; set; }
        public TimeSpan PickupCaptureTimeAvg { get; set; }
        public int PickupCaptureTimeMinTics { get; set; }
        public int PickupCaptureTimeMaxTics { get; set; }
        public double PickupCaptureTimeAvgTics { get; set; }
        public int TotalDamage { get; set; }
        public int SelfDamage { get; set; }
        public int SelfDamageWithFlag { get; set; }
        public int TotalGreenArmorDamage { get; set; }
        public int TotalBlueArmorDamage { get; set; }
        public int TotalDamageToFlagCarriers { get; set; }
        public int TotalDamageAsFlagCarrier { get; set; }
        public int TotalDamageToFlagCarriersWhileHoldingFlag { get; set; }
        public int TotalDamageTakenFromEnvironment { get; set; }
        public int TotalDamageTakenFromEnvironmentAsFlagCarrier { get; set; }
        public int TotalFlagReturns { get; set; }
        public int TotalKills { get; set; }
        public double KillDeathRatio { get; set; }
        public double CapturePercentage { get; set; }
        public double PickupCapturePercentage { get; set; }
        public double OverallCapturePercentage { get; set; }
        public int FlagDefenses { get; set; }
        public int TotalDeaths { get; set; }
        public int Deaths { get; set; }
        public int Suicides { get; set; }
        public int SuicidesWithFlag { get; set; }
        public int EnvironmentalDeaths { get; set; }
        public int EnvironmentalDeathsAsFlagCarrier { get; set; }
        public int TeamKills { get; set; }
        public int LongestSpree { get; set; }
        public int HighestMultiKill { get; set; }
        public int TotalPowerPickups { get; set; }
        public int TotalHealthFromPickups { get; set; }
        public int HealthFromNonPowerPickups { get; set; }
        public int HealthFromPowerPickups { get; set; }
        public int TouchHealthMin { get; set; }
        public int TouchHealthMax { get; set; }
        public double TouchHealthAvg { get; set; }
        public int TouchGreenArmorMin { get; set; }
        public int TouchGreenArmorMax { get; set; }
        public double TouchGreenArmorAvg { get; set; }
        public int TouchBlueArmorMin { get; set; }
        public int TouchBlueArmorMax { get; set; }
        public double TouchBlueArmorAvg { get; set; }
        public int TouchHealthResultCaptureMin { get; set; }
        public int TouchHealthResultCaptureMax { get; set; }
        public double TouchHealthResultCaptureAvg { get; set; }
        public int TouchesOverOneHundredHealth { get; set; }
        public List<Pickup> PickupList { get; set; }
        public List<Damage> DamageList { get; set; }
        public List<Damage> DamageWithFlagList { get; set; }
        public List<Kill> KillsList { get; set; }
        public List<Kill> CarrierKillList { get; set; }
        public List<Accuracy> AccuracyList { get; set; }
        public List<Accuracy> AccuracyWithFlagList { get; set; }
    }
}