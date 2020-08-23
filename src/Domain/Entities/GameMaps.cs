using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class GameMaps
    {
        public uint IdGameMap { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdMap { get; set; }

        public virtual Games FkIdGameNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
    }
}
