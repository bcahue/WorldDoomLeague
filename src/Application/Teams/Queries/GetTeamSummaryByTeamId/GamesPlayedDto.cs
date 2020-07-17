using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamSummaryByTeamId
{
    public class GamesPlayedDto
    {
        public uint GameId { get; set; }
        public string GameName { get; set; }
    }
}
