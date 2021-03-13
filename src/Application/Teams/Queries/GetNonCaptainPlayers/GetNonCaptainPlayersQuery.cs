using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Enums;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorldDoomLeague.Application.Common.Exceptions;

namespace WorldDoomLeague.Application.Teams.Queries.GetNonCaptainPlayers
{
    public class GetNonCaptainPlayersQuery : IRequest<NonCaptainTeamPlayersVm>
    {
        public uint TeamId { get; set; }

        public GetNonCaptainPlayersQuery(uint teamId)
        {
            TeamId = teamId;
        }
    }

    public class GetNonCaptainPlayersQueryHandler : IRequestHandler<GetNonCaptainPlayersQuery, NonCaptainTeamPlayersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetNonCaptainPlayersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NonCaptainTeamPlayersVm> Handle(GetNonCaptainPlayersQuery request, CancellationToken cancellationToken)
        {

            if (await _context.Teams.CountAsync(c => c.IdTeam == request.TeamId) <= 0)
            {
                throw new NotFoundException();
            }

            var dto = new NonCaptainTeamPlayersDto();

            var team = await _context.Teams
                    .Include(i => i.FkIdPlayerFirstpickNavigation)
                    .Include(i => i.FkIdPlayerSecondpickNavigation)
                    .Include(i => i.FkIdPlayerThirdpickNavigation)
                    .Where(w => w.IdTeam == request.TeamId)
                    .FirstOrDefaultAsync(cancellationToken);

            dto.Id = (int)team.IdTeam;

            List<NonCaptainPlayersDto> teamPlayers = new List<NonCaptainPlayersDto>();

            teamPlayers.Add(new NonCaptainPlayersDto { Id = (int)team.FkIdPlayerFirstpickNavigation.Id, PlayerName = team.FkIdPlayerFirstpickNavigation.PlayerName });
            teamPlayers.Add(new NonCaptainPlayersDto { Id = (int)team.FkIdPlayerSecondpickNavigation.Id, PlayerName = team.FkIdPlayerSecondpickNavigation.PlayerName });
            teamPlayers.Add(new NonCaptainPlayersDto { Id = (int)team.FkIdPlayerThirdpickNavigation.Id, PlayerName = team.FkIdPlayerThirdpickNavigation.PlayerName });

            dto.TeamPlayers = teamPlayers;

            return new NonCaptainTeamPlayersVm
            {
                TeamPlayers = dto
            };
        }
    }
}
