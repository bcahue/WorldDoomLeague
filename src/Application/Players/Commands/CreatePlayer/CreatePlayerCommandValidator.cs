using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.PlayerName)
                .NotEmpty().WithMessage("PlayerName is required.")
                .MaximumLength(32).WithMessage("PlayerName must not exceed 32 characters.")
                .MustAsync(BeUniquePlayerName).WithMessage("The specified player name already exists.");

            RuleFor(v => v.PlayerAlias)
                .MaximumLength(32).WithMessage("PlayerAlias must not exceed 32 characters.");
        }

        public async Task<bool> BeUniquePlayerName(string name, CancellationToken cancellationToken)
        {
            return await _context.Player
                .AllAsync(p => p.PlayerName != name);
        }
    }
}
