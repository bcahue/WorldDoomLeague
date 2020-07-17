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

namespace WorldDoomLeague.Application.Players.Queries.GetPlayers
{
    public class GetPlayersQuery : IRequest<PlayersVm>
    {
    }

    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, PlayersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayersVm> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            return new PlayersVm
            {
                PlayerList = await _context.Player
                    .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
