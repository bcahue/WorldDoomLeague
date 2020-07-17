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

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasons
{
    public class GetSeasonsQuery : IRequest<SeasonsVm>
    {
    }

    public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, SeasonsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeasonsVm> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
        {
            return new SeasonsVm
            {
                SeasonList = await _context.Season
                    .ProjectTo<SeasonDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
