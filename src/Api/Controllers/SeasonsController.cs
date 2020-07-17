using WorldDoomLeague.Application.Seasons.Queries.GetSeasons;
using WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById;
using WorldDoomLeague.Application.Seasons.Queries.GetSeasonStandingsById;
using WorldDoomLeague.Application.Seasons.Queries.GetSeasonPlayersBySeasonId;
//using WorldDoomLeague.Application.Seasons.Commands.CreateSeason;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.Api.Controllers
{
    public class SeasonsController : ApiController
    {
        [HttpGet]
        public async Task<SeasonsVm> Get()
        {
            return await Mediator.Send(new GetSeasonsQuery());
        }
        /*
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }
        */
        [HttpGet("{seasonId}")]
        public async Task<SeasonSummaryVm> GetSeasonSummaryById(uint seasonId)
        {
            return await Mediator.Send(new GetSeasonSummaryBySeasonIdQuery(seasonId));
        }

        [HttpGet("{seasonId}/standings")]
        public async Task<SeasonStandingsVm> GetSeasonStandingsById(uint seasonId)
        {
            return await Mediator.Send(new GetSeasonStandingsBySeasonIdQuery(seasonId));
        }

        [HttpGet("{seasonId}/players")]
        public async Task<SeasonPlayersVm> GetSeasonPlayers(uint seasonId)
        {
            return await Mediator.Send(new GetSeasonPlayersBySeasonIdQuery(seasonId));
        }
    }
}
