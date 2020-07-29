using WorldDoomLeague.Application.Players.Queries.GetPlayers;
using WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById;
//using WorldDoomLeague.Application.Players.Queries.GetPlayerComparison;
//using WorldDoomLeague.Application.Players.Commands.CreatePlayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.Api.Controllers
{
    public class PlayersController : ApiController
    {
        [HttpGet]
        public async Task<PlayersVm> Get()
        {
            return await Mediator.Send(new GetPlayersQuery());
        }
        /*
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreatePlayerCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpGet("comparison")]
        public async Task<ComparePlayerSummaryVm> GetPlayerComparison([FromQuery]uint playerId, [FromQuery]uint vsPlayerId)
        {
            return await Mediator.Send(new GetPlayerComparisonQuery(playerId, vsPlayerId));
        }
        */
        [HttpGet("{playerId}/summary")]
        public async Task<PlayerSummaryVm> GetPlayerSummaryById(uint playerId)
        {
            return await Mediator.Send(new GetPlayerSummaryByIdQuery(playerId));
        }
    }
}
