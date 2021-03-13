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

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamPlayers
{
    public class GetTeamPlayersQuery : IRequest<TeamPlayersVm>
    {
        public uint TeamId { get; set; }

        public GetTeamPlayersQuery(uint teamId)
        {
            TeamId = teamId;
        }
    }

    public class GetTeamPlayersQueryHandler : IRequestHandler<GetTeamPlayersQuery, TeamPlayersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeamPlayersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamPlayersVm> Handle(GetTeamPlayersQuery request, CancellationToken cancellationToken)
        {

            if (await _context.Teams.CountAsync(c => c.IdTeam == request.TeamId) <= 0)
            {
                throw new NotFoundException();
            }

            var dto = new TeamPlayersDto();

            var team = await _context.Teams
                    .Include(i => i.FkIdPlayerCaptainNavigation)
                    .Include(i => i.FkIdPlayerFirstpickNavigation)
                    .Include(i => i.FkIdPlayerSecondpickNavigation)
                    .Include(i => i.FkIdPlayerThirdpickNavigation)
                    .Where(w => w.IdTeam == request.TeamId)
                    .FirstOrDefaultAsync(cancellationToken);

            dto.Id = (int)team.IdTeam;

            List<PlayersDto> teamPlayers = new List<PlayersDto>();

            teamPlayers.Add(new PlayersDto { Id = (int)team.FkIdPlayerCaptainNavigation.Id, PlayerName = team.FkIdPlayerCaptainNavigation.PlayerName });
            teamPlayers.Add(new PlayersDto { Id = (int)team.FkIdPlayerFirstpickNavigation.Id, PlayerName = team.FkIdPlayerFirstpickNavigation.PlayerName });
            teamPlayers.Add(new PlayersDto { Id = (int)team.FkIdPlayerSecondpickNavigation.Id, PlayerName = team.FkIdPlayerSecondpickNavigation.PlayerName });
            teamPlayers.Add(new PlayersDto { Id = (int)team.FkIdPlayerThirdpickNavigation.Id, PlayerName = team.FkIdPlayerThirdpickNavigation.PlayerName });

            dto.TeamPlayers = teamPlayers;

            return new TeamPlayersVm
            {
                TeamPlayers = dto
            };
        }
    }
}
