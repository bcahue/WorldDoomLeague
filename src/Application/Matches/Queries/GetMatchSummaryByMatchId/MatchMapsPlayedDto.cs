
namespace WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId
{
    public class MatchMapsPlayedDto
    {
        public uint MapId { get; set; }
        public uint MapNumber { get; set; }
        public string MapName { get; set; }
        public string MapPack { get; set; }
    }
}