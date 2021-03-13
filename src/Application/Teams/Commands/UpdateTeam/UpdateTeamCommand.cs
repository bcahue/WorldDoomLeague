using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorldDoomLeague.Application.Common.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.Teams.Commands.UpdateTeam
{
    public partial class UpdateTeamCommand : IRequest<uint>
    {
        public string TeamName { get; set; }
        public string TeamAbbreviation { get; set; }
        public uint TeamId { get; set; }
    }

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTeamCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Teams.Where(w => w.IdTeam == request.TeamId).FirstOrDefaultAsync(cancellationToken) 
                ?? 
                throw new NotFoundException(nameof(Teams), request.TeamId);

            entity.TeamName = request.TeamName;
            entity.TeamAbbreviation = request.TeamAbbreviation;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdTeam;
        }
    }
}
