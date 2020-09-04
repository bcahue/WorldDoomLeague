using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents a kill event in the WDL log files.
    /// </summary>
    public class KillAggregate
    {
        public int TotalKills { get; set; }
        public string TargetName { get; set; }
        public LogFileEnums.Mods Weapon { get; set; }
    }
}