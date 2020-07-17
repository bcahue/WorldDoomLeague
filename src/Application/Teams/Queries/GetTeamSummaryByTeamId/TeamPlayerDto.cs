using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamSummaryByTeamId
{
    public class TeamPlayerDto
    {
        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}
