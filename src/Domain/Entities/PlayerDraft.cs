using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class PlayerDraft
    {
        public uint DraftRecordId { get; set; }
        public uint FkIdPlayerNominating { get; set; }
        public uint FkIdPlayerNominated { get; set; }
        public uint FkIdPlayerSoldTo { get; set; }
        public uint FkIdTeamSoldTo { get; set; }
        public uint FkIdSeason { get; set; }
        public uint SellPrice { get; set; }
        public uint DraftPosition { get; set; }

        public virtual Player FkIdPlayerNominatingNavigation { get; set; }
        public virtual Player FkIdPlayerNominatedNavigation { get; set; }
        public virtual Player FkIdPlayerSoldToNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Teams FkIdTeamSoldToNavigation { get; set; }
    }
}
