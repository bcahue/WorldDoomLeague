using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Matches.Commands.DeletePlayoffMatch
{
    public class DeletePlayoffMatchCommandValidator : AbstractValidator<DeletePlayoffMatchCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePlayoffMatchCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Match)
                .NotEmpty().WithMessage("Match is required.")
                .MustAsync(BeValidMatch).WithMessage("The specified match does not exist.")
                .MustAsync(BePlayoffMatch).WithMessage("The specified match is not a playoff match.")
                .MustAsync(BeUnfinishedSeason).WithMessage("The specified season where this match exists has been finalized. Please undo the finals game before editing matches within the season.")
                .MustAsync(BeMatchNotPlayed).WithMessage("The specified match has not been played or forfeited. Please Undo this game before deleting.");
        }

        public async Task<bool> BeValidMatch(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match, cancellationToken);
        }

        public async Task<bool> BePlayoffMatch(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match && (p.GameType == "p" || p.GameType == "f"), cancellationToken);
        }

        public async Task<bool> BeUnfinishedSeason(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match && p.FkIdSeasonNavigation.FkIdTeamWinner == null, cancellationToken);
        }

        public async Task<bool> BeMatchNotPlayed(uint match, CancellationToken cancellationToken)
        {
            return await _context.Games
                .AnyAsync(p => p.IdGame == match && (p.FkIdTeamWinner == null && p.TeamWinnerColor == null && p.TeamForfeitColor == null && p.FkIdTeamForfeit == null && p.DoubleForfeit == 0), cancellationToken);
        }
    }
}
