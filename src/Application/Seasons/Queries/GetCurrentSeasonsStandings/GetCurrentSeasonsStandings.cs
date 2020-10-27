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

namespace WorldDoomLeague.Application.Seasons.Queries.GetCurrentSeasonStandings
{
    public class GetCurrentSeasonsStandingsQuery : IRequest<SeasonListVm>
    {
        public GetCurrentSeasonsStandingsQuery()
        {
        }
    }

    public class GetCurrentSeasonsStandingsQueryHandler : IRequestHandler<GetCurrentSeasonsStandingsQuery, SeasonListVm>
    {
        private readonly IApplicationDbContext _context;

        public GetCurrentSeasonsStandingsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SeasonListVm> Handle(GetCurrentSeasonsStandingsQuery request, CancellationToken cancellationToken)
        {
            var SeasonInfo = await _context.Season
                    .Where(w => w.FkIdTeamWinner == null)
                    .ToListAsync(cancellationToken);

            List<SeasonStandingsVm> seasonList = new List<SeasonStandingsVm>();

            foreach (var season in SeasonInfo)
            {
                var seasonStandings = await _context.GameTeamStats
                    .Include(i => i.FkIdTeamNavigation)
                    .Where(w => w.FkIdSeason == season.IdSeason)
                    .ToListAsync(cancellationToken);

                var teams = seasonStandings.Select(s => s.FkIdTeamNavigation.TeamName).Distinct().ToList();

                List<SeasonStandingsDto> standingsList = new List<SeasonStandingsDto>();

                foreach (var team in teams)
                {
                    var teamStanding = seasonStandings.Where(w => w.FkIdTeamNavigation.TeamName == team);

                    standingsList.Add(new SeasonStandingsDto
                    {
                        TeamName = team,
                        Damage = (uint)teamStanding.Sum(s => (s.TotalDamage + s.TotalCarrierDamage)),
                        FlagCapturesAgainst = (uint)teamStanding.Sum(s => s.CapturesAgainst),
                        FlagCapturesFor = (uint)teamStanding.Sum(s => s.CapturesFor),
                        FlagDefenses = (uint)teamStanding.Sum(s => s.TotalCarrierKills),
                        Frags = (uint)teamStanding.Sum(s => (s.TotalKills + s.TotalCarrierKills)),
                        GamesPlayed = (uint)teamStanding.Count(),
                        Losses = (uint)teamStanding.Sum(s => s.Loss),
                        Ties = (uint)teamStanding.Sum(s => s.Tie),
                        Wins = (uint)teamStanding.Sum(s => s.Win),
                        Points = (uint)teamStanding.Sum(s => s.Points),
                        RoundsPlayed = (uint)teamStanding.Sum(s => s.NumberRoundsPlayed),
                        TimePlayed = TimeSpan.FromSeconds(teamStanding.Sum(s => s.NumberTicsPlayed) / 35)
                    });
                }

                seasonList.Add(new SeasonStandingsVm { SeasonStandings = standingsList.OrderByDescending(o => o.Points), SeasonName = season.SeasonName });
            }

            return new SeasonListVm
            {
                Seasons = seasonList
            };
        }
    }
}
