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

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonPlayersBySeasonId
{
    public class GetSeasonPlayersBySeasonIdQuery : IRequest<SeasonPlayersVm>
    {
        public uint Id { get; }

        public GetSeasonPlayersBySeasonIdQuery(uint id)
        {
            Id = id;
        }
    }

    public class GetSeasonPlayersBySeasonIdQueryHandler : IRequestHandler<GetSeasonPlayersBySeasonIdQuery, SeasonPlayersVm>
    {
        private readonly IApplicationDbContext _context;

        public GetSeasonPlayersBySeasonIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SeasonPlayersVm> Handle(GetSeasonPlayersBySeasonIdQuery request, CancellationToken cancellationToken)
        {
            var SeasonInfo = await _context.Season
                    .Where(w => w.IdSeason == request.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? 
                    throw new NotFoundException(string.Format("Season id {0} was not found in the database.", request.Id));

            var seasonPlayerStats = await _context.StatsRounds
                    .Include(i => i.FkIdPlayerNavigation)
                    .Include(i => i.FkIdGameNavigation)
                    .Include(i => i.FkIdSeasonNavigation)
                    .Include(i => i.FkIdRoundNavigation)
                    .Include(i => i.FkIdMapNavigation)
                    .Where(w => w.FkIdSeason == request.Id)
                    .ToListAsync(cancellationToken);

            List<SeasonPlayersDto> playerList = new List<SeasonPlayersDto>();

            var players = seasonPlayerStats.Select(s => s.FkIdPlayerNavigation.PlayerName).Distinct().ToList();

            foreach (var player in players)
            {
                var playerStats = seasonPlayerStats.Where(w => w.FkIdPlayerNavigation.PlayerName == player);

                playerList.Add(new SeasonPlayersDto
                { 
                    PlayerName = player,
                    Assists = (uint)playerStats.Sum(s => s.TotalAssists),
                    Captures = (uint)playerStats.Sum(s => s.TotalCaptures),
                    Damage = (uint)playerStats.Sum(s => (s.TotalDamage + s.TotalDamageFlagCarrier)),
                    Deaths = (uint)playerStats.Sum(s => s.TotalDeaths),
                    FlagDefenses = (uint)playerStats.Sum(s => s.TotalCarrierKills),
                    FlagReturns = (uint)playerStats.Sum(s => s.TotalFlagReturns),
                    FlagTouches = (uint)playerStats.Sum(s => s.TotalTouches),
                    Frags = (uint)playerStats.Sum(s => (s.TotalKills + s.TotalCarrierKills)),
                    GamesPlayed = (uint)playerStats.Select(c => c.FkIdGameNavigation).Count(),
                    RoundsPlayed = (uint)playerStats.Select(c => c.FkIdRoundNavigation).Count(),
                    PickupCaptures = (uint)playerStats.Sum(s => s.TotalPickupCaptures),
                    Points = (uint)playerStats.Sum(s => (s.TotalPickupCaptures + s.TotalCaptures)),
                    Powerups = (uint)playerStats.Sum(s => s.TotalPowerPickups),
                    TimePlayed = TimeSpan.FromSeconds((double)playerStats.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                });
            }

            return new SeasonPlayersVm
            {
                SeasonStandings = playerList.OrderByDescending(o => o.Points)
            };
        }
    }
}
