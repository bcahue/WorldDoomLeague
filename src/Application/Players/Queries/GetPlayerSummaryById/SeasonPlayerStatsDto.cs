using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class SeasonPlayerStatsDto
    {
        public uint SeasonId { get; set; }
        public string SeasonName { get; set; }
        public StatsDto SeasonStats { get; set; }

        public SeasonPlayerStatsDto (uint seasonId, string seasonName, StatsDto seasonStats)
        {
            SeasonId = seasonId;
            SeasonName = seasonName;
            SeasonStats = seasonStats;
        }
    }
}
