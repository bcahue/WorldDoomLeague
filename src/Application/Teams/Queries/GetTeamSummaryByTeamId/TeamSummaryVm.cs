using System.Collections.Generic;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamSummaryByTeamId
{
    public class TeamSummaryVm
    {
        public uint TeamId { get; set; }
        public uint SeasonId { get; set; }
        public string TeamName { get; set; }
        public string SeasonName { get; set; }
        public bool DidTeamWinSeason { get; set; }
        public IEnumerable<TeamDraftDto> Draft { get; set; }
        public IEnumerable<TeamPlayerDto> Players { get; set; }
        public IEnumerable<GamesPlayedDto> GamesPlayed { get; set; }
        public TeamStatsDto Stats { get; set; }
    }
}
