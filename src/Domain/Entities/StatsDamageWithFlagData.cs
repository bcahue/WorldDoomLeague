using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class StatsDamageWithFlagData
    {
        public uint IdStatsCarrierDamage { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdPlayerAttacker { get; set; }
        public uint FkIdPlayerTarget { get; set; }
        public byte WeaponType { get; set; }
        public uint DamageHealth { get; set; }
        public uint DamageGreenArmor { get; set; }
        public uint DamageBlueArmor { get; set; }

        public virtual Player FkIdPlayerAttackerNavigation { get; set; }
        public virtual Player FkIdPlayerTargetNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
    }
}
