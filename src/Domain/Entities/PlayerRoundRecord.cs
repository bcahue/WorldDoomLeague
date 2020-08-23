using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class PlayerRoundRecord
    {
        public uint RoundRecordID { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint FkIdTeam { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdMap { get; set; }
        public uint FkIdStatsRound { get; set; }
        public uint Win { get; set; }
        public uint Tie { get; set; }
        public uint Loss { get; set; }
        public byte AsCaptain { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual Teams FkIdTeamNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
        public virtual StatsRounds FkIdStatsRoundNavigation { get; set; }
    }
}
