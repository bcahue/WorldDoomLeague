using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class PlayerTransactions
    {
        public uint TransactionId { get; set; }
        public uint FkIdTeamTradedFrom { get; set; }
        /// <summary>
        /// Indicates a team the player was traded to. null means free agency.
        /// </summary>
        public uint? FkIdTeamTradedTo { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint FkIdPlayerTradedFor { get; set; }
        public byte PlayerPromotedCaptain { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Player FkIdPlayerTradedForNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual Teams FkIdTeamTradedToNavigation { get; set; }
        public virtual Teams FkIdTeamTradedFromNavigation { get; set; }
    }
}
