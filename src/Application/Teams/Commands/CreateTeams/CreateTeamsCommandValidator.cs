using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Teams.Commands.CreateTeams
{
    public class CreateTeamsCommandValidator : AbstractValidator<CreateTeamsCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTeamsCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(SeasonMustExist).WithMessage("The specified season does not exist.")
                .MustAsync(BeValidSeason).WithMessage("The specified season already has teams.");

            RuleFor(v => v.TeamsRequestList)
                .NotEmpty().WithMessage("TeamsRequestList is required.")
                .Must(BeUniqueTeamAbv).WithMessage("One of the specified abbreviations is a duplicate with another in this request.")
                .Must(BeUniqueTeamName).WithMessage("One of the specified team names is a duplicate with another in this request.")
                .Must(BeUniqueCaptain).WithMessage("One of the specified captains is a duplicate with another in this request.");

            RuleForEach(v => v.TeamsRequestList).ChildRules(team =>
            {
                team.RuleFor(v => v.TeamName)
                    .NotEmpty().WithMessage("TeamName is required.")
                    .MaximumLength(64).WithMessage("TeamName must not exceed 64 characters.");

                team.RuleFor(v => v.TeamAbbreviation)
                    .NotEmpty().WithMessage("TeamAbbreviation is required.")
                    .MaximumLength(5).WithMessage("TeamAbbreviation must not exceed 5 characters.");

                team.RuleFor(v => v.TeamCaptain)
                    .NotEmpty().WithMessage("TeamCaptain is required.");
            });
        }

        public bool BeUniqueTeamAbv(List<TeamsRequest> teams)
        {
            var duplicates = teams.GroupBy(a => a.TeamAbbreviation).Where(a => a.Count() > 1).Select(x => new { TeamAbbreviation = x.Key });

            return !duplicates.Any();
        }

        public bool BeUniqueTeamName(List<TeamsRequest> teams)
        {
            var duplicates = teams.GroupBy(a => a.TeamName).Where(a => a.Count() > 1).Select(x => new { TeamName = x.Key });

            return !duplicates.Any();
        }

        public bool BeUniqueCaptain(List<TeamsRequest> teams)
        {
            var duplicates = teams.GroupBy(a => a.TeamCaptain).Where(a => a.Count() > 1).Select(x => new { TeamCaptain = x.Key });

            return !duplicates.Any();
        }

        public async Task<bool> SeasonMustExist(uint season, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason == season)
                !.AnyAsync(cancellationToken);
        }

        public async Task<bool> BeValidSeason(uint season, CancellationToken cancellationToken)
        {
            return await _context.Teams.Where(w => w.FkIdSeason == season).CountAsync(cancellationToken) <= 0;
        }
    }
}
