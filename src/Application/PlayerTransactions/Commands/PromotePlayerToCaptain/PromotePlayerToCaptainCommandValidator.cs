using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.PromotePlayerToCaptain
{
    public class PromotePlayerToCaptainCommandValidator : AbstractValidator<PromotePlayerToCaptainCommand>
    {
        private readonly IApplicationDbContext _context;

        public PromotePlayerToCaptainCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season isn't in the database.")
                .MustAsync(BeSeasonStillPlaying).WithMessage("The specified season is finalized. Trades must be done while seasons are still in play.");

            RuleFor(v => v.Week)
                .NotEmpty().WithMessage("Week is required.")
                .MustAsync(BeValidWeek).WithMessage("The specified week isn't in the database.");

            RuleFor(v => v.PlayerPromotedCaptain)
                .NotEmpty().WithMessage("PlayerPromotedCaptain is required.")
                .MustAsync(BeValidPlayer).WithMessage("The specified player isn't in the database.")
                .MustAsync(BePlayerOnTeamTradedFrom).WithMessage("The specified player isn't in the team that is specified to trade from.")
                .MustAsync(BeNotACaptainAlready).WithMessage("The specified player is already a captain.");

            RuleFor(v => v.Team)
                .NotEmpty().WithMessage("TeamTradedFrom is required.")
                .MustAsync(BeValidTeam).WithMessage("TeamTradedFrom isn't a valid team from the specified season.");
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

        public async Task<bool> BeValidWeek(PromotePlayerToCaptainCommand request, uint weekId, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                .Where(w => w.IdWeek == weekId && w.FkIdSeason == request.Season)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeValidTeam(PromotePlayerToCaptainCommand request, uint teamId, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.IdTeam == teamId && w.FkIdSeason == request.Season)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeNotACaptainAlready(PromotePlayerToCaptainCommand request, uint playerid, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Where(w => w.IdTeam == request.Team && w.FkIdSeason == request.Season)
                .FirstOrDefaultAsync(cancellationToken);

            if (team.FkIdPlayerCaptain == playerid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> BePlayerOnTeamTradedFrom(PromotePlayerToCaptainCommand request, uint playerId, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Where(w => w.IdTeam == request.Team)
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
