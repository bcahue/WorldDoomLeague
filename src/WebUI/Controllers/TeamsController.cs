using WorldDoomLeague.Application.Teams.Queries.GetTeamSummaryByTeamId;
using WorldDoomLeague.Application.Teams.Commands.CreateTeam;
using WorldDoomLeague.Application.Teams.Queries.GetTeamsBySeasonId;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldDoomLeague.Application.Teams.Commands.CreateTeams;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class TeamsController : ApiController
    {
        [HttpGet("{teamId}/summary")]
        public async Task<TeamSummaryVm> GetTeamSummaryById(uint teamId)
        {
            return await Mediator.Send(new GetTeamSummaryByTeamIdQuery(teamId));
        }

        [HttpGet("{seasonId}")]
        public async Task<TeamsVm> GetTeamsBySeasonId(uint seasonId)
        {
            return await Mediator.Send(new GetTeamsBySeasonIdQuery(seasonId));
        }

        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateTeamCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("teams")]
        public async Task<ActionResult<uint>> CreateTeams(CreateTeamsCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
