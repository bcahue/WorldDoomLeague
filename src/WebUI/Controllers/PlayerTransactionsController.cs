using WorldDoomLeague.Application.PlayerTransaction.Commands.TradePlayerToTeam;
using WorldDoomLeague.Application.PlayerTransaction.Commands.TradePlayerToFreeAgency;
using WorldDoomLeague.Application.PlayerTransaction.Commands.PromotePlayerToCaptain;
using WorldDoomLeague.Application.PlayerTransaction.Commands.ReverseLastTrade;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class PlayerTransactionsController : ApiController
    {
        [HttpPost("TradePlayerToTeam")]
        public async Task<ActionResult<bool>> TradePlayerToTeam(TradePlayerToTeamCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("TradePlayerToFreeAgency")]
        public async Task<ActionResult<bool>> TradePlayerToFreeAgency(TradePlayerToFreeAgencyCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("PromotePlayerToCaptain")]
        public async Task<ActionResult<bool>> PromotePlayerToCaptain(PromotePlayerToCaptainCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ReverseLastTrade")]
        public async Task<ActionResult<bool>> ReverseLastTrade(ReverseLastTradeCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
