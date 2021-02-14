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
using WorldDoomLeague.Application.Common.Exceptions;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class GetSeasonSummaryBySeasonIdQuery : IRequest<SeasonSummaryVm>
    {
        public uint Id { get; }

        public GetSeasonSummaryBySeasonIdQuery(uint id)
        {
            Id = id;
        }
    }

    public class GetSeasonSummaryByIdQueryHandler : IRequestHandler<GetSeasonSummaryBySeasonIdQuery, SeasonSummaryVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSeasonSummaryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeasonSummaryVm> Handle(GetSeasonSummaryBySeasonIdQuery request, CancellationToken cancellationToken)
        {
            var SeasonInfo = await _context.Season
                    .Where(w => w.IdSeason == request.Id)
                    .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(string.Format("Season id {0} was not found in the database.", request.Id));

            return new SeasonSummaryVm
            {
                SeasonSummary = await _context.Season
                    .Include(i => i.Weeks)
                        .ThenInclude(ti => ti.Games)
                            .ThenInclude(ti => ti.FkIdTeamRedNavigation)
                    .Include(i => i.Weeks)
                        .ThenInclude(ti => ti.Games)
                            .ThenInclude(ti => ti.FkIdTeamBlueNavigation)
                    .Include(i => i.Weeks)
                        .ThenInclude(ti => ti.Games)
                            .ThenInclude(ti => ti.GameTeamStats)
                    .Include(i => i.Weeks)
                        .ThenInclude(ti => ti.Games)
                            .ThenInclude(ti => ti.Rounds)
                                .ThenInclude(ti => ti.FkIdMapNavigation)
                    .Where(w => w.IdSeason == request.Id)
                    .ProjectTo<SeasonSummaryDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken)
            };
        }
    }
}
