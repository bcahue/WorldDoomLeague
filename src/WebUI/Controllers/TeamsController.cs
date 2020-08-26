using WorldDoomLeague.Application.Teams.Queries.GetTeamSummaryByTeamId;
using WorldDoomLeague.Application.Teams.Commands.CreateTeam;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class TeamsController : ApiController
    {
        [HttpGet("{teamId}/summary")]
        public async Task<TeamSummaryVm> Get(uint teamId)
        {
            return await Mediator.Send(new GetTeamSummaryByTeamIdQuery(teamId));
        }
        
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateTeamCommand command)
        {
            return await Mediator.Send(command);
        }
        
    }
}
