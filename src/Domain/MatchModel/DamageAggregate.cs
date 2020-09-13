using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.MatchModel
{
    public class DamageAggregate
    {
        public int TotalDamage { get; set; }
        public int TotalDamageGreenArmor { get; set; }
        public int TotalDamageBlueArmor { get; set; }
        public string TargetName { get; set; }
        public LogFileEnums.Mods Weapon { get; set; }
    }
}