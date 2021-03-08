using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Matches.Commands.ScheduleMatch
{
    public class ScheduleMatchCommandValidator : AbstractValidator<ScheduleMatchCommand>
    {
        private readonly IApplicationDbContext _context;

        public ScheduleMatchCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Match)
                .NotEmpty().WithMessage("Match is required.")
                .MustAsync(BeValidMatch).WithMessage("The specified match does not exist.")
                .MustAsync(BeMatchNotPlayed).WithMessage("The specified match has already been played.");

            RuleFor(v => v.GameDateTime)
                .NotEmpty().WithMessage("GameDateTime is required.");
        }

        public async Task<bool> BeValidMatch(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match);
        }

        public async Task<bool> BeMatchNotPlayed(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match && (p.FkIdTeamWinner == null && p.TeamWinnerColor == null && p.TeamForfeitColor == null && p.FkIdTeamForfeit == null && p.DoubleForfeit == 0), cancellationToken);
        }
    }
}
