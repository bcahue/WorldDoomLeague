namespace WorldDoomLeague.Domain.Enums
{
    public static partial class LogFileEnums
    {
        /// <summary>
        /// Specifies types of deaths a player can encounter.
        /// </summary>
        public enum DeathType
        {
            KilledByPlayer = 0,
            Environmental = 1,
            Suicide = 2
            //Disconnect = 3
        }
    }
}