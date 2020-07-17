using System.Collections.Generic;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonStandingsById
{
    public class SeasonStandingsVm
    {
        public IEnumerable<SeasonStandingsDto> SeasonStandings { get; set; }
    }
}
