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

namespace WorldDoomLeague.Application.Matches.Queries.GetUnplayedPlayoffGames
{
    public class GetUnplayedPlayoffGamesQuery : IRequest<UnplayedPlayoffGamesVm>
    {
        public uint SeasonId { get; set; }

        public GetUnplayedPlayoffGamesQuery(uint seasonId)
        {
            SeasonId = seasonId;
        }
    }

    public class GetPlayedGamesQueryHandler : IRequestHandler<GetUnplayedPlayoffGamesQuery, UnplayedPlayoffGamesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayedGamesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UnplayedPlayoffGamesVm> Handle(GetUnplayedPlayoffGamesQuery request, CancellationToken cancellationToken)
        {

            if (await _context.Season.CountAsync(c => c.IdSeason == request.SeasonId) <= 0)
            {
                throw new NotFoundException();
            }

            return new UnplayedPlayoffGamesVm
            {
                UnplayedPlayoffGameList = await _context.Games
                    .Include(i => i.FkIdTeamBlueNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                    .Include(i => i.FkIdWeekNavigation)
                    .Where(w => w.FkIdTeamWinner == null && w.FkIdTeamForfeit == null && w.DoubleForfeit == 0 && w.FkIdSeason == request.SeasonId && (w.GameType == "p" || w.GameType == "f"))
                    .ProjectTo<UnplayedPlayoffGamesDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.WeekNumber)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
