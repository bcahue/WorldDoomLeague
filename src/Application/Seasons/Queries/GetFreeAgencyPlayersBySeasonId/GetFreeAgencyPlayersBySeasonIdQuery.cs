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
using System.Collections.Generic;
using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Application.Common.Models;

namespace WorldDoomLeague.Application.Seasons.Queries.GetFreeAgencyPlayersBySeasonId
{
    public class GetFreeAgencyPlayersBySeasonIdQuery : IRequest<FreeAgencyPlayersVm>
    {
        public uint Id { get; }

        public GetFreeAgencyPlayersBySeasonIdQuery(uint id)
        {
            Id = id;
        }
    }

    public class GetFreeAgencyPlayersBySeasonIdQueryHandler : IRequestHandler<GetFreeAgencyPlayersBySeasonIdQuery, FreeAgencyPlayersVm>
    {
        private readonly IApplicationDbContext _context;

        public GetFreeAgencyPlayersBySeasonIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FreeAgencyPlayersVm> Handle(GetFreeAgencyPlayersBySeasonIdQuery request, CancellationToken cancellationToken)
        {
            var SeasonInfo = await _context.Season
                    .Where(w => w.IdSeason == request.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? 
                    throw new NotFoundException(string.Format("Season id {0} was not found in the database.", request.Id));

            var teamsInPlay = await _context.Teams
                .Include(i => i.FkIdPlayerCaptainNavigation)
                .Include(i => i.FkIdPlayerFirstpickNavigation)
                .Include(i => i.FkIdPlayerSecondpickNavigation)
                .Include(i => i.FkIdPlayerThirdpickNavigation)
                .Where(w => w.FkIdSeason == request.Id)
                .ToListAsync(cancellationToken);

            List<FreeAgencyPlayersDto> freeAgencyPlayerList = new List<FreeAgencyPlayersDto>();

            List<Player> teamPlayerIds = new List<Player>();
            List<Player> freeAgency = new List<Player>();

            foreach (var team in teamsInPlay)
            {
                teamPlayerIds.Add(team.FkIdPlayerCaptainNavigation);
                teamPlayerIds.Add(team.FkIdPlayerFirstpickNavigation);
                teamPlayerIds.Add(team.FkIdPlayerSecondpickNavigation);
                teamPlayerIds.Add(team.FkIdPlayerThirdpickNavigation);
            }

            var playerList = await _context.Player.OrderBy(t => t.Id).ToListAsync(cancellationToken);

            freeAgency = playerList.Except(teamPlayerIds).ToList();

            foreach (var p in freeAgency)
            {
                freeAgencyPlayerList.Add(new FreeAgencyPlayersDto
                {
                    PlayerId = p.Id,
                    PlayerName = p.PlayerName
                });
            }

            return new FreeAgencyPlayersVm
            {
                FreeAgency = freeAgencyPlayerList
            };
        }
    }
}
