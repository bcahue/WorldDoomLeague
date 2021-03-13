using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.ReverseLastTrade
{
    public class ReverseLastTradeCommandValidator : AbstractValidator<ReverseLastTradeCommand>
    {
        private readonly IApplicationDbContext _context;

        public ReverseLastTradeCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season isn't in the database.")
                .MustAsync(BeSeasonStillPlaying).WithMessage("The specified season is finalized. Trades must be done while seasons are still in play.")
                .MustAsync(BeTradeToReverse).WithMessage("There are no trades or roster moves in the specified season.")
                .MustAsync(BeNoGamesSinceLastTrade).WithMessage("The last transaction cannot be reversed because a game in the specified week the trade occured was played. Undo this game to reverse the trade.");
        }

        public async Task<bool> BeValidSeason(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeTradeToReverse(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.PlayerTransactions
                .Where(w => w.FkIdSeason == seasonId)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeNoGamesSinceLastTrade(uint seasonId, CancellationToken cancellationToken)
        {
            var lastTransaction = await _context.PlayerTransactions
                .Where(w => w.FkIdSeason == seasonId)
                .LastOrDefaultAsync(cancellationToken);

            return await _context.Games
                .Where(w => w.FkIdWeek == lastTransaction.FkIdWeek && w.FkIdTeamWinner != null && w.FkIdTeamForfeit != null && w.DoubleForfeit != 1)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeSeasonStillPlaying(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId && w.FkIdTeamWinner == null)
                .AnyAsync(cancellationToken);
        }
    }
}
