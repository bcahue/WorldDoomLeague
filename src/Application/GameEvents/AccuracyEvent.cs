using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.GameEvents
{
    public class AccuracyEvent
    {
        public string ShooterName { get; set; }
        public string TargetName { get; set; }
        public LogFileEnums.Weapons Weapon { get; set; }
        public bool HitMiss { get; set; }
        public double SpritePercent { get; set; }
        public double PinpointPercent { get; set; }
        public int AngleBits { get; set; }
        public int ActivatorX { get; set; }
        public int ActivatorY { get; set; }
        public int ActivatorZ { get; set; }
        public int TargetX { get; set; }
        public int TargetY { get; set; }
        public int TargetZ { get; set; }
    }
}