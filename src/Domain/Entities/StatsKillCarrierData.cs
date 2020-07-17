
namespace WorldDoomLeague.Domain.Entities
{
    public partial class StatsKillCarrierData
    {
        public uint IdStatsKillcarrier { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdPlayerAttacker { get; set; }
        public uint FkIdPlayerTarget { get; set; }
        public byte WeaponType { get; set; }
        public uint TotalKills { get; set; }

        public virtual Player FkIdPlayerAttackerNavigation { get; set; }
        public virtual Player FkIdPlayerTargetNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
    }
}
