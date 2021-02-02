using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Application.Common.Security;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Engine.Commands.CreateEngine
{
    [Authorize(Roles = "Administrator")]
    public partial class CreateEngineCommand : IRequest<uint>
    {
        public string EngineName { get; set; }
        public string EngineUrl { get; set; }
    }

    public class CreateEngineCommandHandler : IRequestHandler<CreateEngineCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateEngineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateEngineCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Engine
            {
                EngineName = request.EngineName,
                EngineUrl = request.EngineUrl
            };

            _context.Engines.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdEngine;
        }
    }
}
