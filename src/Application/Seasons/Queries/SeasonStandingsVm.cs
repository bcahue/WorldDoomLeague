using System.Collections.Generic;

namespace WorldDoomLeague.Application.Seasons.Queries
{
    public class SeasonStandingsVm
    {
        public string SeasonName { get; set; }
        public IEnumerable<SeasonStandingsDto> SeasonStandings { get; set; }
    }
}
