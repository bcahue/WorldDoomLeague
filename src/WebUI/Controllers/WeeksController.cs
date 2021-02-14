using WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks;
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
    }
}
