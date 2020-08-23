using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePlayerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.PlayerName)
                .NotEmpty().WithMessage("PlayerName is required.")
                .MaximumLength(32).WithMessage("PlayerName must not exceed 32 characters.")
                .MustAsync(BeUniquePlayerName).WithMessage("The specified player name already exists.");

            RuleFor(v => v.PlayerAlias)
                .MaximumLength(32).WithMessage("PlayerAlias must not exceed 32 characters.");
        }

        public async Task<bool> BeUniquePlayerName(UpdatePlayerCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Player
                .Where(w => w.Id != model.PlayerId)
                .AllAsync(p => p.PlayerName != name);
        }
    }
}
