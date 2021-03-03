using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Rounds
    {
        public Rounds()
        {
            RoundPlayers = new HashSet<RoundPlayers>();
            PlayerRoundRecords = new HashSet<PlayerRoundRecord>();
            StatsAccuracyData = new HashSet<StatsAccuracyData>();
            StatsAccuracyFlagOutData = new HashSet<StatsAccuracyWithFlagData>();
            StatsDamageCarrierData = new HashSet<StatsDamageWithFlagData>();
            StatsDamageData = new HashSet<StatsDamageData>();
            StatsKillCarrierData = new HashSet<StatsKillCarrierData>();
            StatsKillData = new HashSet<StatsKillData>();
            StatsPickupData = new HashSet<StatsPickupData>();
            StatsRounds = new HashSet<StatsRounds>();
            PlayerRoundOpponents = new HashSet<PlayerRoundOpponent>();
            PlayerRoundTeammates = new HashSet<PlayerRoundTeammate>();
        }

        public uint IdRound { get; set; }
        public uint FkIdMap { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdGame { get; set; }
        public uint? RoundNumber { get; set; }
        public DateTime? RoundDatetime { get; set; }
        public ushort? RoundParseVersion { get; set; }
        public uint? RoundTicsDuration { get; set; }
        public string RoundWinner { get; set; }

        public virtual Games FkIdGameNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual ICollection<RoundPlayers> RoundPlayers { get; set; }
        public virtual ICollection<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        public virtual ICollection<StatsAccuracyData> StatsAccuracyData { get; set; }
        public virtual ICollection<StatsAccuracyWithFlagData> StatsAccuracyFlagOutData { get; set; }
        public virtual ICollection<StatsDamageWithFlagData> StatsDamageCarrierData { get; set; }
        public virtual ICollection<StatsDamageData> StatsDamageData { get; set; }
        public virtual ICollection<StatsKillCarrierData> StatsKillCarrierData { get; set; }
        public virtual ICollection<StatsKillData> StatsKillData { get; set; }
        public virtual ICollection<StatsPickupData> StatsPickupData { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<PlayerRoundOpponent> PlayerRoundOpponents { get; set; }
        public virtual ICollection<PlayerRoundTeammate> PlayerRoundTeammates { get; set; }
    }
}
