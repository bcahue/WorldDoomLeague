using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Matches.Commands.UndoMatch
{
    public class UndoMatchCommandValidator : AbstractValidator<UndoMatchCommand>
    {
        private readonly IApplicationDbContext _context;

        public UndoMatchCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Match)
                .NotEmpty().WithMessage("Match is required.")
                .MustAsync(BeValidMatch).WithMessage("The specified match does not exist.")
                .MustAsync(BeMatchPlayed).WithMessage("The specified match has not been played or forfeited.");
        }

        public async Task<bool> BeValidMatch(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match, cancellationToken);
        }

        public async Task<bool> BeMatchPlayed(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                !.AnyAsync(p => p.IdGame == match && (p.FkIdTeamWinner == null && p.TeamWinnerColor == null && p.TeamForfeitColor == null && p.FkIdTeamForfeit == null && p.DoubleForfeit == 0), cancellationToken);
        }
    }
}
