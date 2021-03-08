using WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks;
using WorldDoomLeague.Application.Weeks.Queries.GetPlayoffWeeks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class WeeksController : ApiController
    {
        [HttpGet("{seasonId}/RegularSeason")]
        public async Task<RegularSeasonWeeksVm> GetRegularSeasonWeeks(uint seasonId)
        {
            return await Mediator.Send(new GetRegularSeasonWeeksQuery(seasonId));
        }
        [HttpGet("{seasonId}/Playoffs")]
        public async Task<PlayoffWeeksVm> GetPlayoffWeeks(uint seasonId)
        {
            return await Mediator.Send(new GetPlayoffWeeksQuery(seasonId));
        }
    }
}
