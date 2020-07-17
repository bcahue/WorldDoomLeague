using System.Collections.Generic;

namespace WorldDoomLeague.Application.LeaderboardStats.Queries
{
    public class LeaderboardStatsDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public double Stat { get; set; }
    }
}
