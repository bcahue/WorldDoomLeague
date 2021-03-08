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

namespace WorldDoomLeague.Application.Weeks.Queries.GetPlayoffWeeks
{
    public class GetPlayoffWeeksQuery : IRequest<PlayoffWeeksVm>
    {
        public uint SeasonId { get; set; }

        public GetPlayoffWeeksQuery(uint seasonId)
        {
            SeasonId = seasonId;
        }
    }

    public class GetPlayoffWeeksQueryHandler : IRequestHandler<GetPlayoffWeeksQuery, PlayoffWeeksVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayoffWeeksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayoffWeeksVm> Handle(GetPlayoffWeeksQuery request, CancellationToken cancellationToken)
        {
            if (await _context.Season.CountAsync(w => w.IdSeason == request.SeasonId) <= 0)
            {
                throw new NotFoundException(String.Format("The season {0} was not found in the system.", request.SeasonId));
            }

            return new PlayoffWeeksVm
            {
                WeekList = await _context.Weeks
                    .Where(w => w.FkIdSeason == request.SeasonId && (w.WeekType == "p" || w.WeekType == "f"))
                    .ProjectTo<PlayoffWeeksDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.WeekNumber)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
