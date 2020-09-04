using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Matches.Commands.ProcessMatch
{
    public class ProcessMatchCommandValidator : AbstractValidator<ProcessMatchCommand>
    {
        private readonly IApplicationDbContext _context;

        public ProcessMatchCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.MatchId)
                .NotEmpty().WithMessage("MatchId is required.")
                .MustAsync(BeValidGame).WithMessage("The specified game does not exist.")
                .MustAsync(BeGameNotPlayed).WithMessage("The specified game has already been played.");

            RuleFor(v => v.FlipTeams)
                .NotEmpty().WithMessage("FlipTeams is required.");

            RuleForEach(v => v.GameRounds)
                .NotEmpty().WithMessage("Rounds is required.")
                .MustAsync(BeValidPlayerIds).WithMessage("The playerids listed for one of the rounds is invalid.")
                .MustAsync(BeValidMap).WithMessage("The map specified is invalid.");
        }

        public async Task<bool> BeGameNotPlayed(uint game, CancellationToken cancellationToken)
        {
            var match = await _context.Games.Where(w => w.IdGame == game).FirstOrDefaultAsync(cancellationToken);
            return match.TeamWinnerColor == null;
        }

        public async Task<bool> BeValidPlayerIds(RoundObject round, CancellationToken cancellationToken)
        {
            foreach (var player in round.BlueTeamPlayerIds.Concat(round.RedTeamPlayerIds))
            {
                if (!await _context.Player.AnyAsync(a => a.Id == player))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> BeValidMap(RoundObject round, CancellationToken cancellationToken)
        {
            return await _context.Maps.AnyAsync(a => a.IdMap == round.Map);
        }

        public async Task<bool> BeValidGame(uint game, CancellationToken cancellationToken)
        {
            return await _context.Games.AnyAsync(a => a.IdGame == game);
        }
    }
}
