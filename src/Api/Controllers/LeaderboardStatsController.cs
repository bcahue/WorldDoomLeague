using WorldDoomLeague.Application.LeaderboardStats.Queries.GetPlayerLeaderboardStatsBySeasonId;
using WorldDoomLeague.Application.LeaderboardStats.Queries.GetPlayerLeaderboardStatsAllTime;
using WorldDoomLeague.Api.QueryModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorldDoomLeague.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace WorldDoomLeague.Api.Controllers
{
    public class LeaderboardStatsController : ApiController
    {
        [HttpGet("{seasonId}/players")]
        [EnumDataType(typeof(LeaderboardStatsMode))]
        public async Task<PlayerLeaderboardSeasonStatsVm> GetSeason(uint seasonId, [FromQuery] LeaderboardStatsMode mode = LeaderboardStatsMode.Total)
        {
            return await Mediator.Send(new GetPlayerLeaderboardStatsBySeasonIdQuery(seasonId, mode));
        }

        [HttpGet("players")]
        [EnumDataType(typeof(LeaderboardStatsMode))]
        public async Task<PlayerLeaderboardAllTimeStatsVm> GetAllTime([FromQuery] LeaderboardStatsMode mode = LeaderboardStatsMode.Total)
        {
            return await Mediator.Send(new GetPlayerLeaderboardStatsAllTimeQuery(mode));
        }
    }
}
