namespace WorldDoomLeague.Domain.GameEvents
{
    public class FlagTouchEvent
    {
        public string PlayerName { get; set; }
        public int ActivatorX { get; set; }
        public int ActivatorY { get; set; }
        public int ActivatorZ { get; set; }
    }
}