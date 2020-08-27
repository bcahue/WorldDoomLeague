namespace WorldDoomLeague.Domain.Enums
{
    public static partial class LogFileEnums
    {
        /// <summary>
        /// Describes events that can occur in the log file.
        /// </summary>
        public enum Event
        {
            Damage = 0,
            CarrierDamage = 1,
            Kill = 2,
            CarrierKill = 3,
            EnvironmentDamage = 4,
            EnvironmentCarrierDamage = 5,
            EnvironmentKill = 6,
            EnvironmentCarrierKill = 7,
            FlagTouch = 8,
            FlagPickupTouch = 9,
            FlagCapture = 10,
            FlagPickupCapture = 11,
            FlagAssist = 12,
            FlagReturn = 13,
            PowerPickup = 14,
            Accuracy = 15
        }
    }
}