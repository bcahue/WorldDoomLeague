using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class MatchSummaryVm
    {
        public uint MatchId { get; set; }
        public uint SeasonId { get; set; }
        public uint RedTeamId { get; set; }
        public uint BlueTeamId { get; set; }
        public int RoundsPlayed { get; set; }
        public TimeSpan GameTimeTotal { get; set; }
        public string SeasonName { get; set; }
        public string RedTeamName { get; set; }
        public string BlueTeamName { get; set; }
        public IEnumerable<MatchMapsPlayedDto> MapsPlayed { get; set;}
        public MatchLineScoreDto LineScore { get; set; }
        public MatchFinalBoxScoreDto FinalBoxScore { get; set; }
        public IEnumerable<RoundBoxScoreDto> PerRoundBoxScore { get; set; }
        public IEnumerable<DemoDto> DemoList { get; set; }
        //public HeatmapDto DeathMap { get; set; }
    }
}
