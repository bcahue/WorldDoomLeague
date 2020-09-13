using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.GameEvents
{
    public class PickupEvent
    {
        public string PlayerName { get; set; }
        public LogFileEnums.Pickups Type { get; set; }
        public int ActivatorX { get; set; }
        public int ActivatorY { get; set; }
        public int ActivatorZ { get; set; }
    }
}