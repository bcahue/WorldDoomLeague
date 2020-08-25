using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTeamCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.TeamName)
                .NotEmpty().WithMessage("TeamName is required.")
                .MaximumLength(64).WithMessage("TeamName must not exceed 64 characters.")
                .MustAsync(BeUniqueTeamName).WithMessage("The specified team name already exists in this specified season.");

            RuleFor(v => v.TeamAbbreviation)
                .NotEmpty().WithMessage("TeamAbbreviation is required.")
                .MaximumLength(4).WithMessage("TeamAbbreviation must not exceed 4 characters.")
                .MustAsync(BeUniqueTeamAbbreviation).WithMessage("The specified team abbreviation already exists in this specified season.");

            RuleFor(v => v.TeamSeason)
                .NotEmpty().WithMessage("TeamSeason is required.")
                .MustAsync(SeasonMustExist).WithMessage("The specified season does not exist.");

            RuleFor(v => v.TeamCaptain)
                .NotEmpty().WithMessage("TeamCaptain is required.")
                .MustAsync(BeUniqueCaptain).WithMessage("The specified captain alreaady has a team in the specified season.");
        }

        public async Task<bool> BeUniqueTeamName(CreateTeamCommand command, string name, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.FkIdSeason == command.TeamSeason)
                .AllAsync(p => p.TeamName != name);
        }

        public async Task<bool> BeUniqueTeamAbbreviation(CreateTeamCommand command, string abbreviation, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.FkIdSeason == command.TeamSeason)
                .AllAsync(p => p.TeamAbbreviation != abbreviation);
        }

        public async Task<bool> SeasonMustExist(uint season, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == season)
                !.AnyAsync();
        }

        public async Task<bool> BeUniqueCaptain(CreateTeamCommand command, uint captain, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.FkIdSeason == command.TeamSeason)
                .AllAsync(p => p.FkIdPlayerCaptain != captain);
        }
    }
}
