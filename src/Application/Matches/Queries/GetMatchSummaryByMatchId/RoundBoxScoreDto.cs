using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class RoundBoxScoreDto
    {
        public int RoundNumber { get; set; }
        public TimeSpan RoundTimeTotal { get; set; }
        public MatchStatsDto RedTeamBoxScore { get; set; }
        public MatchStatsDto BlueTeamBoxScore { get; set; }
        public IEnumerable<GamePlayersDto> RedTeamPlayerRoundBoxScore { get; set; }
        public IEnumerable<GamePlayersDto> BlueTeamPlayerRoundBoxScore { get; set; }
    }
}
