using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class PlayerGameOpponent
    {
        public uint PlayerGameOpponentId { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint FkIdTeam { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdOpponent { get; set; }
        public uint FkIdPlayerGameRecord { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Player FkIdOpponentNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
        public virtual Teams FkIdTeamNavigation { get; set; }
        public virtual PlayerGameRecord FkIdPlayerGameRecordNavigation { get; set; }
    }
}
