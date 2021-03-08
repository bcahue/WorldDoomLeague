using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Teams.Commands.AssignTeamHomefield
{
    public class AssignTeamHomefieldValidator : AbstractValidator<AssignTeamHomefieldCommand>
    {
        private readonly IApplicationDbContext _context;

        public AssignTeamHomefieldValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.TeamId)
                .NotEmpty().WithMessage("TeamId is required.")
                .MustAsync(TeamIdMustExist).WithMessage("The specified team id does not exist.");

            RuleFor(v => v.MapId)
                .NotEmpty().WithMessage("MapId is required.")
                .MustAsync(BeUniqueHomefieldMapForThatSeason).WithMessage("The specified map id is already assigned to another team that season.");
        }

        public async Task<bool> TeamIdMustExist(uint teamid, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Where(w => w.IdTeam == teamid)
                !.AnyAsync(cancellationToken);
        }

        public async Task<bool> BeUniqueHomefieldMapForThatSeason(AssignTeamHomefieldCommand command, uint mapid, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.Where(w => w.IdTeam == command.TeamId).FirstOrDefaultAsync(cancellationToken);

            var seasonTeams = await _context.Teams.Where(w => w.FkIdSeason == team.FkIdSeason).ToListAsync(cancellationToken);

            return seasonTeams.Count(a => a.FkIdHomefieldMap == command.MapId && a.IdTeam != command.TeamId) <= 0;
        }
    }
}
