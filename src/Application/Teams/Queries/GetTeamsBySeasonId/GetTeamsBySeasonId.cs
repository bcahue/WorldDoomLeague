using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamsBySeasonId
{
    public class GetTeamsBySeasonIdQuery : IRequest<TeamsVm>
    {
        public uint SeasonId { get; }

        public GetTeamsBySeasonIdQuery(uint seasonId)
        {
            SeasonId = seasonId;
        }
    }

    public class GetTeamsBySeasonIdQueryHandler : IRequestHandler<GetTeamsBySeasonIdQuery, TeamsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamsBySeasonIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamsVm> Handle(GetTeamsBySeasonIdQuery request, CancellationToken cancellationToken)
        {
            if (await _context.Teams.CountAsync(c => c.FkIdSeason == request.SeasonId, cancellationToken) <= 0)
            {
                throw new NotFoundException();
            }

            return new TeamsVm
            {
                TeamList = await _context.Teams
                    .Where(w => w.FkIdSeason == request.SeasonId)
                    .ProjectTo<TeamsDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
