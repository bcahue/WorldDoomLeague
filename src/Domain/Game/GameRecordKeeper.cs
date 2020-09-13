using System.Collections.Generic;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.Game
{
    public class GameRecordKeeper
    {
        public IList<uint> RedTeamPlayerGameIds { get; private set; }
        public IList<uint> BlueTeamPlayerGameIds { get; private set; }
        public LogFileEnums.GameResult GameResult { get; private set; }
        public IList<RoundResults> RoundResults { get; private set; }

        public GameRecordKeeper(IList<RoundResults> results)
        {
            RoundResults = new List<RoundResults>(results);
            // Process game results here.
        }
    }
}