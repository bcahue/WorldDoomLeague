using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Demos
    {
        public uint DemoId { get; set; }
        public uint FkGameId { get; set; }
        public uint FkPlayerId { get; set; }
        public byte IsUploaded { get; set; }
        public byte PlayerLostDemo { get; set; }

        public virtual Games FkGame { get; set; }
        public virtual Player FkPlayer { get; set; }
    }
}
