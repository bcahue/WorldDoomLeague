﻿using WorldDoomLeague.Application.Files.Commands.CreateWadFile;
using WorldDoomLeague.Application.Files.Commands.CreateMapImageFile;
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
        [RequestSizeLimit(209715200)]
        [HttpPost("wad")]
        public async Task<ActionResult<uint>> CreateWadFile(IFormFile file)
        {
            return await Mediator.Send(new CreateWadFileCommand(file));
        }

        [HttpPost("mapimage")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<uint>> CreateMapImage([FromForm] CreateMapImageFileCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize]
        [HttpGet("wads")]
        public async Task<WadFilesVm> GetWadFiles()
        {
            return await Mediator.Send(new GetWadFilesQuery());
        }

        [HttpGet("json")]
        public async Task<IEnumerable<string>> GetRoundJsonFiles()
        {
            return await Mediator.Send(new GetRoundJsonFilesQuery());
        }

        [HttpGet("json/rounddata")]
        public async Task<Round> GetRoundObject([FromQuery(Name = "FileName")] string fileName)
        {
            return await Mediator.Send(new GetRoundObjectQuery(fileName));
        }
    }
}
