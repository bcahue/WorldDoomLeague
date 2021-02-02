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

namespace WorldDoomLeague.Application.Engine.Queries.GetEngines
{
    public class GetEnginesQuery : IRequest<EnginesVm>
    {
    }

    public class GetSeasonsQueryHandler : IRequestHandler<GetEnginesQuery, EnginesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EnginesVm> Handle(GetEnginesQuery request, CancellationToken cancellationToken)
        {
            return new EnginesVm
            {
                EngineList = await _context.Engines
                    .ProjectTo<EnginesDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
