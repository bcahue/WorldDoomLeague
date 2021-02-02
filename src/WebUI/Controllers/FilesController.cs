using WorldDoomLeague.Application.Files.Commands.CreateWadFile;
using WorldDoomLeague.Application.Files.Queries.GetRoundJsonFiles;
using WorldDoomLeague.Application.Files.Queries.GetRoundObject;
using WorldDoomLeague.Application.Files.Queries.GetWadFiles;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldDoomLeague.Domain.MatchModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class FilesController : ApiController
    {
        [Authorize]
        [HttpPost("wad")]
        public async Task<ActionResult<uint>> CreateWadFile(IFormFile file)
        {
            return await Mediator.Send(new CreateWadFileCommand(file));
        }

        [Authorize]
        [HttpGet("wads")]
        public async Task<WadFilesVm> GetWadFiles()
        {
            return await Mediator.Send(new GetWadFilesQuery());
        }

        [Authorize]
        [HttpGet("json")]
        public async Task<IEnumerable<string>> GetRoundJsonFiles()
        {
            return await Mediator.Send(new GetRoundJsonFilesQuery());
        }

        [Authorize]
        [HttpGet("json/rounddata")]
        public async Task<Round> GetRoundObject([FromQuery(Name = "FileName")] string fileName)
        {
            return await Mediator.Send(new GetRoundObjectQuery(fileName));
        }
    }
}
