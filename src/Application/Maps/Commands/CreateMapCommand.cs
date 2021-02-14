using WorldDoomLeague.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Maps.Commands.CreateMap
{
    public partial class CreateMapCommand : IRequest<uint>
    {
        public string MapName { get; set; }
        public string MapPack { get; set; }
        public uint MapNumber { get; set; }
    }

    public class CreateMapCommandHandler : IRequestHandler<CreateMapCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateMapCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateMapCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Maps
            {
                MapName = request.MapName,
                MapNumber = request.MapNumber,
                MapPack = request.MapPack
            };

            _context.Maps.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdMap;
        }
    }
}
