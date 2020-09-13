using System.Collections.Generic;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.Game
{
    public class RoundResults
    {
        public IList<uint> RedRoundPlayerIds { get; set; }
        public IList<uint> BlueRoundPlayerIds { get; set; }
        public LogFileEnums.GameResult RoundResult { get; set; }

        public override string ToString()
        {
            if (RoundResult == LogFileEnums.GameResult.BlueWin)
            {
                return "b";
            }
            else if (RoundResult == LogFileEnums.GameResult.RedWin)
            {
                return "r";
            }
            else
            {
                return "t";
            }
        }
    }
}