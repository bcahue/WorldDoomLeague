using System.Collections.Generic;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonPlayersBySeasonId
{
    public class SeasonPlayersVm
    {
        public IEnumerable<SeasonPlayersDto> SeasonStandings { get; set; }
    }
}
