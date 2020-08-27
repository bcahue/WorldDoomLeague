using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents a damage event in the WDL log files.
    /// </summary>
    public class Damage
    {
        /// <summary>
        /// Name of the player damaged in this event.
        /// </summary>
        public string DamagedName { get; set; }

        /// <summary>
        /// Type of damage in this event.
        /// </summary>
        public LogFileEnums.Mods DamageType { get; set; }

        /// <summary>
        /// Type of armor the damaged player has in this event.
        /// </summary>
        public LogFileEnums.Armor ArmorType { get; set; }

        /// <summary>
        /// Health points taken away during this damage event.
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// Armor amount taken away during this damage event, depending on armor type.
        /// </summary>
        public int Armor { get; set; }
    }
}