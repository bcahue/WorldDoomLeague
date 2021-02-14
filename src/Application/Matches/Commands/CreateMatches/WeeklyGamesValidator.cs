using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatches
{
    public class WeeklyGamesValidator : AbstractValidator<WeeklyRequest>
    {
        private readonly IApplicationDbContext _context;

        public WeeklyGamesValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.MapId)
                .NotEmpty().WithMessage("MapId is required.")
                .MustAsync(BeValidMap).WithMessage("The specified map id {PropertyValue} does not exist.");

            RuleFor(v => v.WeekId)
                .NotEmpty().WithMessage("WeekId is required.")
                .MustAsync(BeValidWeek).WithMessage("The specified week id {PropertyValue} does not exist.")
                .MustAsync(HaveNoGamesOnWeek).WithMessage("The specified week id {PropertyValue} already has games scheduled.")
                .MustAsync(BeRegularSeasonWeek).WithMessage("The specified week id {PropertyValue} is not a regular season week.");

            RuleFor(v => v.GameList)
                .NotEmpty().WithMessage("GameList is required.")
                .Must(HaveEvenAmountOfGamesPerTeam).WithMessage("The specified GameList has a team playing in more games than the other teams in a week.");

            RuleForEach(v => v.GameList)
                .SetValidator(new NewGameValidator());
        }

        public async Task<bool> BeValidMap(uint map, CancellationToken cancellationToken)
        {
            return await _context.Maps
                .AnyAsync(p => p.IdMap == map);
        }

        public async Task<bool> BeValidWeek(uint week, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                .AnyAsync(p => p.IdWeek == week);
        }

        public async Task<bool> BeRegularSeasonWeek(uint week, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                .AnyAsync(p => p.IdWeek == week && p.WeekType == "n");
        }

        public async Task<bool> HaveNoGamesOnWeek(uint week, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AllAsync(p => p.FkIdWeek != week);
        }

        // TODO: update this to have it check if there's only 1 week.
        public bool HaveEvenAmountOfGamesPerTeam(List<NewGame> weeklyGames)
        {
            List<NewGame> originalList = new List<NewGame>(weeklyGames);

            foreach (var game in originalList)
            {
                List<NewGame> referenceList = new List<NewGame>(weeklyGames);
                referenceList.Remove(game);

                foreach (var check in referenceList)
                {
                    if (game.BlueTeam == check.BlueTeam || game.BlueTeam == check.RedTeam ||
                        game.RedTeam == check.BlueTeam || game.RedTeam == check.RedTeam)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
