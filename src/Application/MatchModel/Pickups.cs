using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents an item pickup event in the WDL log files.
    /// </summary>
    public class Pickup
    {
        public LogFileEnums.Pickups PickupType { get; set; }
        public int PickupX { get; set; }
        public int PickupY { get; set; }
        public int PickupZ { get; set; }
    }
}