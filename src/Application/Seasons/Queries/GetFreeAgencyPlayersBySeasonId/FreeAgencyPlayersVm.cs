using System.Collections.Generic;

namespace WorldDoomLeague.Application.Seasons.Queries.GetFreeAgencyPlayersBySeasonId
{
    public class FreeAgencyPlayersVm
    {
        public IEnumerable<FreeAgencyPlayersDto> FreeAgency { get; set; }
    }
}
