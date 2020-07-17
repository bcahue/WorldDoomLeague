using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class MatchLineScoreDto
    {
        public string RedTeamName { get; set; }
        public string BlueTeamName { get; set; }
        public IEnumerable<ScoreDto> RoundScore { get; set; }
        public MatchResultDto MatchResult { get; set; }
    }
}
