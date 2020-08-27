using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace WorldDoomLeague.Application.Seasons.Commands.UpdateSeason
{
    public partial class UpdateSeasonCommand : IRequest
    {
        public uint SeasonId { get; set; }
        public uint WadId { get; set; }
        public string SeasonName { get; set; }
        public DateTime DateStart { get; set; }
    }

    public class UpdateSeasonCommandHandler : IRequestHandler<UpdateSeasonCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSeasonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Season.FindAsync(request.SeasonId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Season), request.SeasonId);
            }

            entity.SeasonName  = request.SeasonName;
            entity.FkIdWadFile = request.WadId;
            entity.DateStart   = request.DateStart;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
