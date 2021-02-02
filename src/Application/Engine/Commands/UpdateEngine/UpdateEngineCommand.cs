using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace WorldDoomLeague.Application.Engine.Commands.UpdateEngine
{
    public partial class UpdateEngineCommand : IRequest
    {
        public uint EngineId { get; set; }
        public string EngineName { get; set; }
        public string EngineUrl { get; set; }
    }

    public class UpdateSeasonCommandHandler : IRequestHandler<UpdateEngineCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSeasonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEngineCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Engines.FindAsync(request.EngineId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Engine), request.EngineId);
            }

            entity.EngineName  = request.EngineName;
            entity.EngineUrl = request.EngineUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
