namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class MatchResultDto
    {
        public int RedRoundScore { get; set; }
        public int BlueRoundScore { get; set; }
        public string GameWinner { get; set; }
    }
}