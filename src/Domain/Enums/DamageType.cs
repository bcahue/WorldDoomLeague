namespace WorldDoomLeague.Domain.Enums
{
    public static partial class LogFileEnums
    {
        /// <summary>
        /// Specifies types of damage a player can encounter.
        /// </summary>
        public enum DamageType
        {
            DamageByEnemyPlayer = 0,
            DamageByTeammate = 1,
            SelfDamage = 2,
            EnvironmentalDamage = 3
            //DisconnectDamage = 4
        }
    }
}