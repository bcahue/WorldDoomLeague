using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents a kill event in the WDL log files.
    /// </summary>
    public class Kill
    {
        public LogFileEnums.Mods KilledByWeapon { get; set; }
        public string KilledName { get; set; }
        public int KilledX { get; set; }
        public int KilledY { get; set; }
        public int KilledZ { get; set; }
        public int FraggerX { get; set; }
        public int FraggerY { get; set; }
        public int FraggerZ { get; set; }
    }
}