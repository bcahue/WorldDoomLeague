using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class PlayerGameTeammate
    {
        public uint PlayerGameTeammateId { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint FkIdTeam { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdTeammate { get; set; }
        public uint FkIdPlayerGameRecord { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Player FkIdTeammateNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
        public virtual Teams FkIdTeamNavigation { get; set; }
        public virtual PlayerGameRecord FkIdPlayerGameRecordNavigation { get; set; }
    }
}
