using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class WeekMaps
    {
        public uint IdWeekMap { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdMap { get; set; }

        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
    }
}
