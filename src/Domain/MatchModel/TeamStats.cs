using System.Collections.Generic;

namespace WorldDoomLeague.Domain.MatchModel
{
    public class TeamStats
    {
        public int Points { get; set; }
        public int Captures { get; set; }
        public int PickupCaptures { get; set; }
        public int Assists { get; set; }
        public int FlagTouches { get; set; }
        public int PickupFlagTouches { get; set; }
        public double TotalCapturePercentage { get; set; }
        public double PickupCapturePercentage { get; set; }
        public double CapturePercentage { get; set; }
        public int Frags { get; set; }
        public int Deaths { get; set; }
        public double KillDeathRatio { get; set; }
        public int Damage { get; set; }
        public int FlagDefenses { get; set; }
        public int PowerPickups { get; set; }
        public IList<string> TeamPlayers { get; private set; }
    }
}