using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.GameEvents
{
    public class KillEvent
    {
        public string ShooterName { get; set; }
        public string TargetName { get; set; }
        public LogFileEnums.Mods Weapon { get; set; }
        public int ActivatorX { get; set; }
        public int ActivatorY { get; set; }
        public int ActivatorZ { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }
    }
}