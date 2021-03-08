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
            return await _context
                .Games
                .CountAsync(p => (p.FkIdTeamWinner != null || p.FkIdTeamForfeit != null || p.DoubleForfeit == 1) &&
                (p.FkIdSeasonNavigation.FkIdTeamWinner == null || p.GameType == "f"), cancellationToken) > 0;
        }
    }
}
