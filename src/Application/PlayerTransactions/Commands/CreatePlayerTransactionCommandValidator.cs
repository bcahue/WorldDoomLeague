using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.CreatePlayerTransaction
{
    public class CreatePlayerTransactionCommandValidator : AbstractValidator<CreatePlayerTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayerTransactionCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.TradedPlayer)
                .NotEmpty().WithMessage("TradedPlayer is required.")
                .MustAsync(BeValidPlayer).WithMessage("The specified player isn't in the database.");

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season isn't in the database.")
                .MustAsync(BeSeasonStillPlaying).WithMessage("The specified season is finalized. Trades can affect stats and player records, and must be done while seasons are still in play.");

            RuleFor(v => v.Week)
                .NotEmpty().WithMessage("Week is required.")
                .MustAsync(BeValidWeek).WithMessage("The specified week isn't in the database.");

            RuleFor(v => v.TeamTradedFrom)
                .Must(BeBothTeamsNotNull).WithMessage("TeamTradedFrom and TeamTradedTo are both null, which should not happen.")
                .MustAsync(BeAValidTeamIfNotNull).WithMessage("TeamTradedFrom didn't have a valid team from this season.");

            RuleFor(v => v.TeamTradedTo)
                .MustAsync(BeAValidTeamIfNotNull).WithMessage("TeamTradedTo didn't have a valid team from this season.");

            RuleFor(v => v.PlayerPromotedCaptain)
                .NotEmpty().WithMessage("PlayerPromotedCaptain is required.")
                .MustAsync(BeValidTeamTradedTo).WithMessage("PlayerPromotedCaptain requires a team in the TeamTradedTo field.");
        }

        public async Task<bool> BeValidPlayer(uint playerId, CancellationToken cancellationToken)
        {
            return await _context.Player
                .Where(w => w.Id == playerId)
                .AnyAsync();
        }

        public async Task<bool> BeValidSeason(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId)
                .AnyAsync();
        }

        public async Task<bool> BeSeasonStillPlaying(uint seasonId, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == seasonId && w.FkIdTeamWinner == null)
                .AnyAsync();
        }

        public async Task<bool> BeValidWeek(CreatePlayerTransactionCommand request, uint weekId, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                .Where(w => w.IdWeek == weekId && w.FkIdSeason == request.Season)
                .AnyAsync();
        }

        public bool BeBothTeamsNotNull(CreatePlayerTransactionCommand request, uint? team)
        {
            return !(request.TeamTradedFrom == null && request.TeamTradedTo == null);
        }

        public async Task<bool> BeAValidTeamIfNotNull(CreatePlayerTransactionCommand request, uint? teamId, CancellationToken cancellationToken)
        {
            if (teamId == null)
            {
                return true;
            } else
            {
                return await _context.Teams
                    .Where(w => w.IdTeam == teamId && w.FkIdSeason == request.Season)
                    .AnyAsync();
            }
        }

        public async Task<bool> BeValidTeamTradedTo(CreatePlayerTransactionCommand request, bool promoted, CancellationToken cancellationToken)
        {
            if (request.TeamTradedTo == null)
            {
                return false;
            } else
            {
                return await _context.Teams
                    .Where(w => w.IdTeam == request.TeamTradedTo && w.FkIdSeason == request.Season)
                    .AnyAsync();
            }
        }
    }
}
