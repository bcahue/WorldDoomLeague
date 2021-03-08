using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WorldDoomLeague.Application.Teams.Commands.AssignTeamHomefield
{
    public partial class AssignTeamHomefieldCommand : IRequest<uint>
    {
        public uint TeamId { get; set; }
        public uint MapId { get; set; }
    }

    public class AssignTeamHomefieldCommandHandler : IRequestHandler<AssignTeamHomefieldCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public AssignTeamHomefieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(AssignTeamHomefieldCommand request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.Where(f => f.IdTeam == request.TeamId).FirstOrDefaultAsync(cancellationToken);

            team.FkIdHomefieldMap = request.MapId;

            await _context.SaveChangesAsync(cancellationToken);

            return team.IdTeam;
        }
    }
}
