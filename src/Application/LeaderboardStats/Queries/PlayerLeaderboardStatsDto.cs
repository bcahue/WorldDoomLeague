using System.Collections.Generic;

namespace WorldDoomLeague.Application.LeaderboardStats.Queries
{
    public class PlayerLeaderboardStatsDto
    {
        public string StatName { get; set; }
        public IEnumerable<LeaderboardStatsDto> LeaderboardStats { get; set; }
    }
}
