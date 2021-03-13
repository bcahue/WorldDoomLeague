using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTeamCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.TeamId)
                .NotEmpty().WithMessage("TeamId is required.")
                .MustAsync(BeValidTeam).WithMessage("TeamId is not found within the specified season.");

            RuleFor(v => v.TeamName)
                .NotEmpty().WithMessage("TeamName is required.")
                .MaximumLength(64).WithMessage("TeamName must not exceed 64 characters.")
                .MustAsync(BeUniqueTeamName).WithMessage("The specified team name already exists in this specified season.");

            RuleFor(v => v.TeamAbbreviation)
                .NotEmpty().WithMessage("TeamAbbreviation is required.")
                .MaximumLength(5).WithMessage("TeamAbbreviation must not exceed 5 characters.")
                .MustAsync(BeUniqueTeamAbbreviation).WithMessage("The specified team abbreviation already exists in this specified season.");
        }

        public async Task<bool> BeValidTeam(uint teamId, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.IdTeam == teamId)
                .AnyAsync(cancellationToken);
        }

        public async Task<bool> BeUniqueTeamName(UpdateTeamCommand command, string name, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Where(w => w.IdTeam == command.TeamId)
                .FirstOrDefaultAsync(cancellationToken);

            return await _context.Teams
                .Where(w => w.IdTeam != command.TeamId && team.FkIdSeason == w.FkIdSeason)
                .AllAsync(p => p.TeamName != name);
        }

        public async Task<bool> BeUniqueTeamAbbreviation(UpdateTeamCommand command, string abbreviation, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Where(w => w.IdTeam == command.TeamId)
                .FirstOrDefaultAsync(cancellationToken);

            return await _context.Teams
                .Where(w => w.FkIdSeason == team.FkIdSeason)
                .AllAsync(p => p.TeamAbbreviation != abbreviation);
        }
    }
}
