using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class StatsPickupData
    {
        public uint IdStatPickup { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdActivatorPlayer { get; set; }
        public byte PickupType { get; set; }
        public uint PickupAmount { get; set; }

        public virtual Player FkIdActivatorPlayerNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
    }
}
