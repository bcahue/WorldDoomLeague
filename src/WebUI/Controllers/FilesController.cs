using WorldDoomLeague.Application.Files.Commands.CreateFile;
using WorldDoomLeague.Application.Files.Queries.GetRoundJsonFiles;
using WorldDoomLeague.Application.Files.Queries.GetRoundObject;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WorldDoomLeague.WebUI.ConfigModels;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using WorldDoomLeague.Application.MatchModel;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class FilesController : ApiController
    {
        private readonly DataDirectories _options;

        public FilesController(IOptions<DataDirectories> options)
        {
            _options = options.Value;
        }

        /*
        [HttpGet]
        public async Task<PlayersVm> Get()
        {
            return await Mediator.Send(new GetPlayersQuery());
        }
        
        [HttpGet("comparison")]
        public async Task<ComparePlayerSummaryVm> GetPlayerComparison([FromQuery]uint playerId, [FromQuery]uint vsPlayerId)
        {
            return await Mediator.Send(new GetPlayerComparisonQuery(playerId, vsPlayerId));
        }
        
        [HttpGet("{playerId}/summary")]
        public async Task<PlayerSummaryVm> GetPlayerSummaryById(uint playerId)
        {
            return await Mediator.Send(new GetPlayerSummaryByIdQuery(playerId));
        }
        */
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateFileCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("json")]
        public async Task<IEnumerable<string>> GetRoundJsonFiles()
        {
            return await Mediator.Send(new GetRoundJsonFilesQuery(_options.JsonMatchDirectory));
        }

        [HttpGet("json/rounddata")]
        public async Task<Round> GetRoundObject([FromQuery(Name = "FileName")] string fileName)
        {
            return await Mediator.Send(new GetRoundObjectQuery(_options.JsonMatchDirectory, fileName));
        }
        /*
        [HttpPut("{playerId}")]
        public async Task<ActionResult> Update(uint playerId, UpdatePlayerCommand command)
        {
            if (playerId != command.PlayerId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
        */
    }
}
