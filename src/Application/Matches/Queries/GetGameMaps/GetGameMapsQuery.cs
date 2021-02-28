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

namespace WorldDoomLeague.Application.Matches.Queries.GetGameMaps
{
    public class GetGameMapsQuery : IRequest<GameMapsVm>
    {
        public uint MatchId { get; set; }

        public GetGameMapsQuery(uint matchId)
        {
            MatchId = matchId;
        }
    }

    public class GetGameMapsQueryHandler : IRequestHandler<GetGameMapsQuery, GameMapsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetGameMapsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GameMapsVm> Handle(GetGameMapsQuery request, CancellationToken cancellationToken)
        {

            if (await _context.Games.CountAsync(c => c.IdGame == request.MatchId) <= 0)
            {
                throw new NotFoundException();
            }

            return new GameMapsVm
            {
                GameMaps = await _context.GameMaps
                    .Include(i => i.FkIdMapNavigation)
                    .Where(w => w.FkIdGame == request.MatchId)
                    .ProjectTo<GameMapsDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
