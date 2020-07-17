using System.Collections.Generic;

namespace WorldDoomLeague.Application.LeaderboardStats.Queries.GetPlayerLeaderboardStatsBySeasonId
{
    public class PlayerLeaderboardSeasonStatsVm
    {
        public uint SeasonId { get; set; }
        public string SeasonName { get; set; }
        public IEnumerable<PlayerLeaderboardStatsDto> PlayerLeaderboardStats { get; set; }
    }
}
