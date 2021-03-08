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

namespace WorldDoomLeague.Application.Matches.Queries.GetPlayedGames
{
    public class GetPlayedGamesQuery : IRequest<PlayedGamesVm>
    {
        public GetPlayedGamesQuery()
        {
        }
    }

    public class GetPlayedGamesQueryHandler : IRequestHandler<GetPlayedGamesQuery, PlayedGamesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayedGamesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayedGamesVm> Handle(GetPlayedGamesQuery request, CancellationToken cancellationToken)
        {
            return new PlayedGamesVm
            {
                PlayedGameList = await _context.Games
                    .Include(i => i.FkIdTeamBlueNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                    .Include(i => i.FkIdTeamWinnerNavigation)
                    .Include(i => i.FkIdWeekNavigation)
                    .Include(i => i.FkIdSeasonNavigation)
                    .Where(w => (w.FkIdTeamWinner != null || w.FkIdTeamForfeit != null || w.DoubleForfeit == 1) && // get all games not played or forfeited...
                    (w.FkIdSeasonNavigation.FkIdTeamWinner == null || w.GameType == "f")) // skip games that are complete unless its a finals game.
                    .ProjectTo<PlayedGamesDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.WeekNumber)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
