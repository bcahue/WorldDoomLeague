//using WorldDoomLeague.Application.Maps.Queries.GetMapSummaryById;
using WorldDoomLeague.Application.Maps.Commands.CreateMap;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.Api.Controllers
{
    public class MapsController : ApiController
    {
        /*
        [HttpGet("{seasonId}")]
        public async Task<TeamSummaryVm> Get(uint seasonId)
        {
            return await Mediator.Send(new GetDraftBySeasonId(seasonId));
        }
        */
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateMapCommand command)
        {
            return await Mediator.Send(command);
        }
        
    }
}
