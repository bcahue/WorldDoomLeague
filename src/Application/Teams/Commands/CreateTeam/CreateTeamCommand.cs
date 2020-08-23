using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Teams.Commands.CreateTeam
{
    public partial class CreateTeamCommand : IRequest<uint>
    {
        public string TeamName { get; set; }
        public string TeamAbbreviation { get; set; }
        public uint TeamSeason { get; set; }
        public uint TeamCaptain { get; set; }
    }

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateTeamCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Teams
            {
                FkIdSeason = request.TeamSeason,
                FkIdPlayerCaptain = request.TeamCaptain,
                TeamName = request.TeamName,
                TeamAbbreviation = request.TeamAbbreviation
            };

            _context.Teams.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdTeam;
        }
    }
}
