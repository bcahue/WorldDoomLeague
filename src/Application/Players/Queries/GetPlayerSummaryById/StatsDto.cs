using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class StatsDto
    {
        public int RoundsPlayed { get; set; }
        public TimeSpan TotalTimePlayed { get; set; }
        public int TotalFrags { get; set; }
        public int Frags { get; set; }
        public int FlagCarrierFrags { get; set; }
        public int Deaths { get; set; }
        public int EnvironmentalDeaths { get; set; }
        public int Damage { get; set; }
        public int FlagCarrierDamage { get; set; }
        public int DamageWithFlag { get; set; }
        public int FlagTouches { get; set; }
        public int PickupFlagTouches { get; set; }
        public int Assists { get; set; }
        public int Captures { get; set; }
        public int PickupCaptures { get; set; }
        public int FlagReturns { get; set; }
        public int PowerPickups { get; set; }
        public int KillingSprees { get; set; }
        public int Rampages { get; set; }
        public int Dominations { get; set; }
        public int Unstoppables { get; set; }
        public int GodLikes { get; set; }
        public int WickedSicks { get; set; }
        public int DoubleKills { get; set; }
        public int MultiKills { get; set; }
        public int UltraKills { get; set; }
        public int MonsterKills { get; set; }

        public StatsDto(
            int roundsPlayed,
            TimeSpan totalTimePlayed,
            int totalFrags,
            int frags,
            int flagCarrierFrags,
            int deaths,
            int environmentalDeaths,
            int damage,
            int flagCarrierDamage,
            int damageWithFlag,
            int flagTouches,
            int pickupFlagTouches,
            int assists,
            int captures,
            int pickupCaptures,
            int flagReturns,
            int powerPickups,
            int killingSprees,
            int rampages,
            int dominations,
            int unstoppables,
            int godLikes,
            int wickedSicks,
            int doubleKills,
            int multiKills,
            int ultraKills,
            int monsterKills)
        {
            RoundsPlayed = roundsPlayed;
            TotalTimePlayed = totalTimePlayed;
            TotalFrags = totalFrags;
            Frags = frags;
            FlagCarrierFrags = flagCarrierFrags;
            Deaths = deaths;
            EnvironmentalDeaths = environmentalDeaths;
            Damage = damage;
            FlagCarrierDamage = flagCarrierDamage;
            DamageWithFlag = damageWithFlag;
            FlagTouches = flagTouches;
            PickupFlagTouches = pickupFlagTouches;
            Assists = assists;
            Captures = captures;
            PickupCaptures = pickupCaptures;
            FlagReturns = flagReturns;
            PowerPickups = powerPickups;
            KillingSprees = killingSprees;
            Rampages = rampages;
            Dominations = dominations;
            Unstoppables = unstoppables;
            GodLikes = godLikes;
            WickedSicks = wickedSicks;
            DoubleKills = doubleKills;
            MultiKills = multiKills;
            UltraKills = ultraKills;
            MonsterKills = monsterKills;
        }
    }
}
