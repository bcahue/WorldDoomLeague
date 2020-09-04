using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents an accuracy event in the WDL log files.
    /// </summary>
    public class AccuracyAggregate
    {
        public double PinpointPercentage { get; set; }
        public double SpritePercentage { get; set; }
        public int TotalHits { get; set; }
        public int TotalMisses { get; set; }
        public double HitMissRatio { get; set; }
        public LogFileEnums.Weapons Weapon { get; set; }
    }
}