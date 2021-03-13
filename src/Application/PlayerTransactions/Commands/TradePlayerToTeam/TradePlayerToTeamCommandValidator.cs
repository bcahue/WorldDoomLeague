using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.TradePlayerToTeam
{
    public class TradePlayerToTeamCommandValidator : AbstractValidator<TradePlayerToTeamCommand>
    {
        private readonly IApplicationDbContext _context;

        public TradePlayerToTeamCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season isn't in the database.")
                .MustAsync(BeSeasonStillPlaying).WithMessage("The specified season is finalized. Trades must be done while seasons are still in play.");

            RuleFor(v => v.Week)
                .NotEmpty().WithMessage("Week is required.")
                .MustAsync(BeValidWeek).WithMessage("The specified week isn't in the database.");

            RuleFor(v => v.TradedPlayer)
                .NotEmpty().WithMessage("TradedPlayer is required.")
                .MustAsync(BeValidPlayer).WithMessage("The specified player isn't in the database.")
                .MustAsync(BePlayerOnTeamTradedFrom).WithMessage("The specified player isn't in the team that is specified to trade from.");

            RuleFor(v => v.TradedPlayerFor)
                .NotEmpty().WithMessage("TradedPlayer is required.")
                .MustAsync(BeValidPlayer).WithMessage("The specified player isn't in the database.")
                .MustAsync(BePlayerOnTeamTradedTo).WithMessage("The specified player isn't in the team that is specified to trade from.");

            RuleFor(v => v.TeamTradedFrom)
                .MustAsync(BeValidTeam).WithMessage("TeamTradedFrom didn't have a valid team from this season.");

            RuleFor(v => v.TeamTradedTo)
                .MustAsync(BeValidTeam).WithMessage("TeamTradedTo didn't have a valid team from this season.");
        }

        public async Task<bool> BeValidPlayer(uint playerId, CancellationToken cancellationToken)
        {
            return await _context.Player
                .Where(w => w.Id == playerId)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeValidSeason(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeSeasonStillPlaying(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId && w.FkIdTeamWinner == null)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeValidWeek(TradePlayerToTeamCommand request, uint weekId, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                .Where(w => w.IdWeek == weekId && w.FkIdSeason == request.Season)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeValidTeam(TradePlayerToTeamCommand request, uint teamId, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.IdTeam == teamId && w.FkIdSeason == request.Season)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BePlayerOnTeamTradedFrom(TradePlayerToTeamCommand request, uint playerId, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Where(w => w.IdTeam == request.TeamTradedFrom)
                .FirstOrDefaultAsync(cancellationToken);

            if (team.FkIdPlayerCaptain == playerId)
            {
                return true;
            } else if (team.FkIdPlayerFirstpick == playerId)
            {
                return true;
            }
            else if (team.FkIdPlayerSecondpick == playerId)
            {
                return true;
            }
            else if (team.FkIdPlayerThirdpick == playerId)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<bool> BePlayerOnTeamTradedTo(TradePlayerToTeamCommand request, uint playerId, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Where(w => w.IdTeam == request.TeamTradedTo)
                .FirstOrDefaultAsync(cancellationToken);

            if (team.FkIdPlayerCaptain == playerId)
            {
                return true;
            }
            else if (team.FkIdPlayerFirstpick == playerId)
            {
                return true;
            }
            else if (team.FkIdPlayerSecondpick == playerId)
            {
                return true;
            }
            else if (team.FkIdPlayerThirdpick == playerId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
