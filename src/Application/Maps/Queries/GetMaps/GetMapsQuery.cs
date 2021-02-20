using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Maps.Queries.GetMaps
{
    public class GetMapsQuery : IRequest<MapsVm>
    {
    }

    public class GetMapsQueryHandler : IRequestHandler<GetMapsQuery, MapsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMapsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MapsVm> Handle(GetMapsQuery request, CancellationToken cancellationToken)
        {
            return new MapsVm
            {
                MapList = await _context.Maps
                    .ProjectTo<MapsDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
