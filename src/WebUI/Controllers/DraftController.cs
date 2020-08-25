//using WorldDoomLeague.Application.Draft.Queries.GetDraftBySeasonId;
using WorldDoomLeague.Application.Draft.Commands.CreateDraft;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.Api.Controllers
{
    public class DraftController : ApiController
    {
        /*
        [HttpGet("{seasonId}")]
        public async Task<TeamSummaryVm> Get(uint seasonId)
        {
            return await Mediator.Send(new GetDraftBySeasonId(seasonId));
        }
        */
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDraftCommand command)
        {
            return await Mediator.Send(command);
        }
        
    }
}
