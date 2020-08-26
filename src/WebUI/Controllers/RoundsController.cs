using WorldDoomLeague.Application.Rounds.Queries.ExportRounds;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldDoomLeague.WebUI.QueryModel;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class RoundsController : ApiController
    {
        [HttpGet]
        public async Task<FileResult> Get([FromQuery] RoundsOutputFileTypeQueryModel output)
        {
            var vm = await Mediator.Send(new ExportRoundsQuery(output.Output));

            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }
}
