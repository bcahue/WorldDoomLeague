using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents an item pickup event in the WDL log files.
    /// </summary>
    public class PickupAggregate
    {
        public LogFileEnums.Pickups PickupType { get; set; }
        public int TotalPickups { get; set; }
    }
}