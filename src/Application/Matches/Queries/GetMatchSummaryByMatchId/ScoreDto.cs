namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class ScoreDto
    {
        public string Round { get; set; }
        public int RedScore { get; set; }
        public int BlueScore { get; set; }
        public string RoundWinner { get; set; }
    }
}