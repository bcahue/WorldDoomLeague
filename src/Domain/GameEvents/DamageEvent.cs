using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.GameEvents
{
    public class DamageEvent
    {
        public string ShooterName { get; set; }
        public string TargetName { get; set; }
        public LogFileEnums.Mods DamageType { get; set; }
        public int ActivatorX { get; set; }
        public int ActivatorY { get; set; }
        public int ActivatorZ { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }
        public int Hp { get; set; }
        public int BlueArmor { get; set; }
        public int GreenArmor { get; set; }
    }
}