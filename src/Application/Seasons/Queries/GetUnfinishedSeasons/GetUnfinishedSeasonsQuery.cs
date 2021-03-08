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

namespace WorldDoomLeague.Application.Seasons.Queries.GetUnfinishedSeasons
{
    public class GetUnfinishedSeasonsQuery : IRequest<UnfinishedSeasonsVm>
    {
    }

    public class GetUnfinishedSeasonsQueryHandler : IRequestHandler<GetUnfinishedSeasonsQuery, UnfinishedSeasonsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUnfinishedSeasonsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UnfinishedSeasonsVm> Handle(GetUnfinishedSeasonsQuery request, CancellationToken cancellationToken)
        {
            return new UnfinishedSeasonsVm
            {
                SeasonList = await _context.Season
                    .Where(w => w.FkIdTeamWinner == null)
                    .ProjectTo<UnfinishedSeasonDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
