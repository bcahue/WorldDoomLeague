using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class StatsAccuracyData
    {
        public uint IdStatsAccuracyData { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdPlayerAttacker { get; set; }
        public byte WeaponType { get; set; }
        public double HitMissRatio { get; set; }
        public double SpritePercent { get; set; }
        public double PinpointPercent { get; set; }

        public virtual Player FkIdPlayerAttackerNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
    }
}
