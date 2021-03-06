﻿using WorldDoomLeague.Application.Seasons.Queries.GetSeasons;
using WorldDoomLeague.Application.Seasons.Queries;
using WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById;
using WorldDoomLeague.Application.Seasons.Queries.GetSeasonStandingsById;
using WorldDoomLeague.Application.Seasons.Queries.GetSeasonPlayersBySeasonId;
using WorldDoomLeague.Application.Seasons.Queries.GetUnfinishedSeasons;
using WorldDoomLeague.Application.Seasons.Queries.GetFreeAgencyPlayersBySeasonId;
using WorldDoomLeague.Application.Seasons.Commands.CreateSeason;
using WorldDoomLeague.Application.Seasons.Commands.UpdateSeason;
using WorldDoomLeague.Application.Weeks.Commands.CreateSeasonWeeks;
using WorldDoomLeague.Application.Seasons.Queries.GetCurrentSeasonStandings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class SeasonsController : ApiController
    {
        [HttpGet]
        public async Task<SeasonsVm> Get()
        {
            return await Mediator.Send(new GetSeasonsQuery());
        }
        
        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{seasonId}")]
        public async Task<ActionResult> Update(uint seasonId, UpdateSeasonCommand command)
        {
            if (seasonId != command.SeasonId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("CreateWeeks")]
        public async Task<ActionResult<uint>> CreateWeeks(CreateSeasonWeeksCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{seasonId}")]
        public async Task<SeasonSummaryVm> GetSeasonSummaryById(uint seasonId)
        {
            return await Mediator.Send(new GetSeasonSummaryBySeasonIdQuery(seasonId));
        }

        [HttpGet("current/standings")]
        public async Task<SeasonListVm> GetCurrentSeasonsStandings()
        {
            return await Mediator.Send(new GetCurrentSeasonsStandingsQuery());
        }

        [HttpGet("{seasonId}/standings")]
        public async Task<SeasonStandingsVm> GetSeasonStandingsById(uint seasonId)
        {
            return await Mediator.Send(new GetSeasonStandingsBySeasonIdQuery(seasonId));
        }

        [HttpGet("unfinished")]
        public async Task<UnfinishedSeasonsVm> GetUnfinishedSeasons()
        {
            return await Mediator.Send(new GetUnfinishedSeasonsQuery());
        }

        [HttpGet("{seasonId}/players")]
        public async Task<SeasonPlayersVm> GetSeasonPlayers(uint seasonId)
        {
            return await Mediator.Send(new GetSeasonPlayersBySeasonIdQuery(seasonId));
        }

        [HttpGet("{seasonId}/freeagency")]
        public async Task<FreeAgencyPlayersVm> GetFreeAgencyForSeason(uint seasonId)
        {
            return await Mediator.Send(new GetFreeAgencyPlayersBySeasonIdQuery(seasonId));
        }
    }
}
