using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class GamePlayersDto
    {
        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }
        public MatchStatsDto MatchStats { get; set; }
    }
}