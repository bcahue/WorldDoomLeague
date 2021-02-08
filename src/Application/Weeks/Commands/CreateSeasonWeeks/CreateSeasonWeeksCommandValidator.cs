using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.SeasonWeeks.Commands.CreateSeasonWeeks
{
    public class CreateSeasonWeeksCommandValidator : AbstractValidator<CreateSeasonWeeksCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateSeasonWeeksCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.SeasonId)
                .NotEmpty().WithMessage("SeasonId is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season was not found.")
                .MustAsync(BeSeasonWithoutWeeks).WithMessage("The specified season is already configured with Weeks!");

            RuleFor(v => v.WeekOneDateStart)
                .NotEmpty().WithMessage("WeekOneDateStart is required.");

            RuleFor(v => v.NumWeeksRegularSeason)
                .NotEmpty().WithMessage("NumWeeksRegularSeason is required.");

            RuleFor(v => v.NumWeeksPlayoffs)
                .NotEmpty().WithMessage("NumWeeksPlayoffs is required.");
        }

        public async Task<bool> BeValidSeason(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId)
                .AnyAsync();
        }

        public async Task<bool> BeSeasonWithoutWeeks(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                .Where(w => w.FkIdSeason == seasonId)
                !.AnyAsync();
        }
    }
}
