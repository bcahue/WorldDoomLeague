using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class MatchFinalBoxScoreDto
    {
        public MatchStatsDto RedTeamFinalBoxScore { get; set; }
        public MatchStatsDto BlueTeamFinalBoxScore { get; set; }
        public IEnumerable<GamePlayersDto> RedTeamPlayerFinalBoxScore { get; set; }
        public IEnumerable<GamePlayersDto> BlueTeamPlayerFinalBoxScore { get; set; }
    }
}