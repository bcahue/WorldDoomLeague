using WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId;
//using WorldDoomLeague.Application.Matches.Commands.CreateMatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class MatchesController : ApiController
    {
        [HttpGet("{matchId}/summary")]
        public async Task<MatchSummaryVm> Get(uint matchId)
        {
            return await Mediator.Send(new GetMatchSummaryByMatchIdQuery(matchId));
        }
/*
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }
*/
    }
}
