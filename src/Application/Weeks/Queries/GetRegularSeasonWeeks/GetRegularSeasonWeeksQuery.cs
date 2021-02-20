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

namespace WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks
{
    public class GetRegularSeasonWeeksQuery : IRequest<RegularSeasonWeeksVm>
    {
        public uint SeasonId { get; set; }

        public GetRegularSeasonWeeksQuery(uint seasonId)
        {
            SeasonId = seasonId;
        }
    }

    public class GetRegularSeasonWeeksQueryHandler : IRequestHandler<GetRegularSeasonWeeksQuery, RegularSeasonWeeksVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRegularSeasonWeeksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegularSeasonWeeksVm> Handle(GetRegularSeasonWeeksQuery request, CancellationToken cancellationToken)
        {
            if (await _context.Season.CountAsync(w => w.IdSeason == request.SeasonId) <= 0)
            {
                throw new NotFoundException(String.Format("The season {0} was not found in the system.", request.SeasonId));
            }

            return new RegularSeasonWeeksVm
            {
                WeekList = await _context.Weeks
                    .Where(w => w.FkIdSeason == request.SeasonId && w.WeekType == "n")
                    //.Include(i => i.WeekMaps)
                        //.ThenInclude(ti => ti.FkIdMapNavigation)
                            //.ThenInclude(ti => ti.MapImages)
                                //.ThenInclude(ti => ti.FkIdImageFileNavigation)
                    .ProjectTo<RegularSeasonWeeksDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.WeekNumber)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
