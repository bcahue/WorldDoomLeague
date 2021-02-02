using WorldDoomLeague.Application.Engine.Queries.GetEngines;
using WorldDoomLeague.Application.Engine.Commands.CreateEngine;
using WorldDoomLeague.Application.Engine.Commands.UpdateEngine;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WorldDoomLeague.WebUI.Controllers
{
    [Authorize]
    public class EngineController : ApiController
    {
        [HttpGet]
        public async Task<EnginesVm> Get()
        {
            return await Mediator.Send(new GetEnginesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateEngineCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{engineId}")]
        public async Task<ActionResult> Update(uint engineId, UpdateEngineCommand command)
        {
            if (engineId != command.EngineId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
