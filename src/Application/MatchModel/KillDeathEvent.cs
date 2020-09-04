using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    public class KillDeathEvent
    {
        public string KillerName { get; set; }
        public int KillerX { get; set; }
        public int KillerY { get; set; }
        public int KillerZ { get; set; }
        public string TargetName { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }
        public LogFileEnums.Mods Weapon { get; set; }
    }
}
