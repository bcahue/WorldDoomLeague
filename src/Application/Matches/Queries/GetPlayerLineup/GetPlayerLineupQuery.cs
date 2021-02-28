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

namespace WorldDoomLeague.Application.Matches.Queries.GetPlayerLineup
{
    public class GetPlayerLineupQuery : IRequest<PlayerLineupVm>
    {
        public uint MatchId { get; set; }

        public GetPlayerLineupQuery(uint matchId)
        {
            MatchId = matchId;
        }
    }

    public class GetPlayerLineupQueryHandler : IRequestHandler<GetPlayerLineupQuery, PlayerLineupVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerLineupQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlayerLineupVm> Handle(GetPlayerLineupQuery request, CancellationToken cancellationToken)
        {

            if (await _context.Games.CountAsync(c => c.IdGame == request.MatchId) <= 0)
            {
                throw new NotFoundException();
            }

            var dto = new PlayerLineupDto();

            var lineup = await _context.Games
                    .Include(i => i.FkIdTeamBlueNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerCaptainNavigation)
                    .Include(i => i.FkIdTeamBlueNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerFirstpickNavigation)
                    .Include(i => i.FkIdTeamBlueNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerSecondpickNavigation)
                    .Include(i => i.FkIdTeamBlueNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerThirdpickNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerCaptainNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerFirstpickNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerSecondpickNavigation)
                    .Include(i => i.FkIdTeamRedNavigation)
                        .ThenInclude(ti => ti.FkIdPlayerThirdpickNavigation)
                    .Where(w => w.IdGame == request.MatchId)
                    .OrderBy(t => t.IdGame)
                    .FirstOrDefaultAsync(cancellationToken);

            dto.Id = (int)lineup.IdGame;
            dto.RedTeamName = lineup.FkIdTeamRedNavigation.TeamName;
            dto.BlueTeamName = lineup.FkIdTeamBlueNavigation.TeamName;

            List<TeamPlayersDto> redPlayers = new List<TeamPlayersDto>();
            List<TeamPlayersDto> bluePlayers = new List<TeamPlayersDto>();

            redPlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamRedNavigation.FkIdPlayerCaptainNavigation.Id, PlayerName = lineup.FkIdTeamRedNavigation.FkIdPlayerCaptainNavigation.PlayerName });
            redPlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamRedNavigation.FkIdPlayerFirstpickNavigation.Id, PlayerName = lineup.FkIdTeamRedNavigation.FkIdPlayerFirstpickNavigation.PlayerName });
            redPlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamRedNavigation.FkIdPlayerSecondpickNavigation.Id, PlayerName = lineup.FkIdTeamRedNavigation.FkIdPlayerSecondpickNavigation.PlayerName });
            redPlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamRedNavigation.FkIdPlayerThirdpickNavigation.Id, PlayerName = lineup.FkIdTeamRedNavigation.FkIdPlayerThirdpickNavigation.PlayerName });

            bluePlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamBlueNavigation.FkIdPlayerCaptainNavigation.Id, PlayerName = lineup.FkIdTeamBlueNavigation.FkIdPlayerCaptainNavigation.PlayerName });
            bluePlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamBlueNavigation.FkIdPlayerFirstpickNavigation.Id, PlayerName = lineup.FkIdTeamBlueNavigation.FkIdPlayerFirstpickNavigation.PlayerName });
            bluePlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamBlueNavigation.FkIdPlayerSecondpickNavigation.Id, PlayerName = lineup.FkIdTeamBlueNavigation.FkIdPlayerSecondpickNavigation.PlayerName });
            bluePlayers.Add(new TeamPlayersDto { Id = (int)lineup.FkIdTeamBlueNavigation.FkIdPlayerThirdpickNavigation.Id, PlayerName = lineup.FkIdTeamBlueNavigation.FkIdPlayerThirdpickNavigation.PlayerName });

            dto.RedTeamPlayers = redPlayers;
            dto.BlueTeamPlayers = bluePlayers;

            return new PlayerLineupVm
            {
                GamePlayerLineup = dto
            };
        }
    }
}
