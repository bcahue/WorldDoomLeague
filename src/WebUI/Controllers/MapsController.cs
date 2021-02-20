//using WorldDoomLeague.Application.Maps.Queries.GetMapSummaryById;
using WorldDoomLeague.Application.Maps.Commands.CreateMap;
using WorldDoomLeague.Application.Maps.Queries.GetMaps;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class MapsController : ApiController
    {
        [HttpGet]
        public async Task<MapsVm> Get()
        {
            return await Mediator.Send(new GetMapsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateMapCommand command)
        {
            return await Mediator.Send(command);
        }
        
    }
}
