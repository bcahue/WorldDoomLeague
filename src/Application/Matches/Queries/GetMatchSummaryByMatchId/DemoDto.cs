namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class DemoDto
    {
        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }
        public bool DemoLost { get; set; }
        public string DemoFilePath { get; set; }
    }
}