using WorldDoomLeague.Application.Matches.Queries.GetMatchSummaryByMatchId;
using WorldDoomLeague.Application.Matches.Queries.GetUnplayedGames;
using WorldDoomLeague.Application.Matches.Queries.GetPlayedGames;
using WorldDoomLeague.Application.Matches.Queries.GetPlayerLineup;
using WorldDoomLeague.Application.Matches.Queries.GetGameMaps;
using WorldDoomLeague.Application.Matches.Commands.CreateMatch;
using WorldDoomLeague.Application.Matches.Commands.UndoMatch;
using WorldDoomLeague.Application.Matches.Commands.CreateMatches;
using WorldDoomLeague.Application.Matches.Commands.ProcessMatch;
using WorldDoomLeague.Application.Matches.Commands.ForfeitMatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WorldDoomLeague.WebUI.Controllers
{
    public class MatchesController : ApiController
    {
        [HttpGet("{matchId}/summary")]
        public async Task<MatchSummaryVm> Get(uint matchId)
        {
            return await Mediator.Send(new GetMatchSummaryByMatchIdQuery(matchId));
        }

        [HttpGet("{matchId}/lineup")]
        public async Task<PlayerLineupVm> GetPlayerLineup(uint matchId)
        {
            return await Mediator.Send(new GetPlayerLineupQuery(matchId));
        }

        [HttpGet("{seasonId}/unplayed")]
        public async Task<UnplayedGamesVm> GetUnplayedGames(uint seasonId)
        {
            return await Mediator.Send(new GetUnplayedGamesQuery(seasonId));
        }

        [HttpGet("played")]
        public async Task<PlayedGamesVm> GetPlayedGames()
        {
            return await Mediator.Send(new GetPlayedGamesQuery());
        }

        [HttpGet("{matchId}/maps")]
        public async Task<GameMapsVm> GetGameMaps(uint matchId)
        {
            return await Mediator.Send(new GetGameMapsQuery(matchId));
        }

        [HttpPost]
        public async Task<ActionResult<uint>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("CreateRegularSeason")]
        public async Task<ActionResult<uint>> CreateRegularSeason(CreateMatchesCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("process")]
        public async Task<ActionResult<uint>> Process(ProcessMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("undo")]
        public async Task<ActionResult<bool>> Undo(UndoMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forfeit")]
        public async Task<ActionResult<bool>> Forfeit(ForfeitMatchCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
