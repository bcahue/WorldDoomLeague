using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class StatsOverall
    {
        public uint IdOverallStats { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint NumberRoundsPlayed { get; set; }
        public uint NumberTicsPlayed { get; set; }
        public uint TotalKills { get; set; }
        public uint TotalCarrierKills { get; set; }
        public uint TotalDeaths { get; set; }
        public uint TotalEnvironmentDeaths { get; set; }
        public uint TotalDamage { get; set; }
        public uint TotalCarrierDamage { get; set; }
        public uint TotalDamageWithFlag { get; set; }
        public uint TotalTouches { get; set; }
        public uint TotalPickupTouches { get; set; }
        public uint TotalAssists { get; set; }
        public uint TotalCaptures { get; set; }
        public uint TotalPickupCaptures { get; set; }
        public uint TotalFlagReturns { get; set; }
        public uint TotalSpreeKillingSprees { get; set; }
        public uint TotalSpreeRampages { get; set; }
        public uint TotalSpreeDominations { get; set; }
        public uint TotalSpreeUnstoppables { get; set; }
        public uint TotalSpreeGodlikes { get; set; }
        public uint TotalSpreeWickedsicks { get; set; }
        public uint TotalMultiDoubleKills { get; set; }
        public uint TotalMultiMultiKills { get; set; }
        public uint TotalMultiUltraKills { get; set; }
        public uint TotalMultiMonsterKills { get; set; }
        public uint TotalPowerPickups { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
    }
}
