using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents an accuracy event in the WDL log files.
    /// </summary>
    public class Accuracy
    {
        /// <summary>
        /// Type of weapon in this event.
        /// </summary>
        public LogFileEnums.Weapons Weapon { get; set; }

        /// <summary>
        /// Represents if the shot hit anything or not.
        /// </summary>
        public bool HitMiss { get; set; }

        /// <summary>
        /// Represents the shot accuracy percentage relative to the enemy sprite.
        /// </summary>
        public double SpritePercent { get; set; }

        /// <summary>
        /// Represents the shot accuracy percentage directly to the center of an enemy.
        /// </summary>
        public double PinpointPercent { get; set; }
    }
}