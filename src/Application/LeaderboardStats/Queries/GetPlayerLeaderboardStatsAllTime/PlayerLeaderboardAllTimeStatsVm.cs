using System.Collections.Generic;

namespace WorldDoomLeague.Application.LeaderboardStats.Queries.GetPlayerLeaderboardStatsAllTime
{
    public class PlayerLeaderboardAllTimeStatsVm
    {
        public string Description = "All Time Stats";
        public IEnumerable<PlayerLeaderboardStatsDto> PlayerLeaderboardStats { get; set; }
    }
}
