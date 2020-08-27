namespace WorldDoomLeague.Domain.Enums
{
    public static partial class LogFileEnums
    {
        /// <summary>
        /// Indicates the winner of the game.
        /// </summary>
        public enum GameResult
        {
            TieGame = -1,
            BlueWin = 0,
            RedWin = 1
        }
    }
}