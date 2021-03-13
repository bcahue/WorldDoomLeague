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

namespace WorldDoomLeague.Application.Matches.Queries.GetUpcomingMatches
{
    public class GetUpcomingMatchesQuery : IRequest<UpcomingMatchesVm>
    {
        public GetUpcomingMatchesQuery()
        {
        }
    }

    public class GetPlayedGamesQueryHandler : IRequestHandler<GetUpcomingMatchesQuery, UpcomingMatchesVm>
    {
        private readonly IApplicationDbContext _context;

        public GetPlayedGamesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UpcomingMatchesVm> Handle(GetUpcomingMatchesQuery request, CancellationToken cancellationToken)
        {
            List<UpcomingMatchesDto> upcoming = new List<UpcomingMatchesDto>();

            var upcomingMatches = await _context.Games
                .Include(i => i.FkIdTeamBlueNavigation)
                    .ThenInclude(i => i.GameTeamStats)
                .Include(i => i.FkIdTeamRedNavigation)
                    .ThenInclude(i => i.GameTeamStats)
                .Include(i => i.FkIdWeekNavigation)
                .Include(i => i.FkIdSeasonNavigation)
                .Include(i => i.GameMaps)
                    .ThenInclude(i => i.FkIdMapNavigation)
                        .ThenInclude(i => i.MapImages)
                            .ThenInclude(i => i.FkIdImageFileNavigation)
                .Where(w => w.FkIdTeamWinner == null && w.FkIdTeamForfeit == null && w.DoubleForfeit == 0)
                .ToListAsync(cancellationToken);

            foreach (var match in upcomingMatches)
            {
                List<UpcomingMapsDto> maps = new List<UpcomingMapsDto>();
                if (match.GameDatetime != null)
                {
                    var redteamRecord = match.FkIdTeamRedNavigation.GameTeamStats.ToList();
                    var blueteamRecord = match.FkIdTeamRedNavigation.GameTeamStats.ToList();

                    var rwins = redteamRecord.Sum(s => s.Win);
                    var rtie = redteamRecord.Sum(s => s.Tie);
                    var rloss = redteamRecord.Sum(s => s.Loss);

                    var bwins = blueteamRecord.Sum(s => s.Win);
                    var btie = blueteamRecord.Sum(s => s.Tie);
                    var bloss = blueteamRecord.Sum(s => s.Loss);

                    string redRecord = $"{rwins}-{rloss}-{rtie}";
                    string blueRecord = $"{bwins}-{bloss}-{btie}";

                    string gameType = "";

                    if (match.GameType == "n")
                    {
                        gameType = $"Regular Season Week #{match.FkIdWeekNavigation.WeekNumber}";
                    } else if (match.GameType == "p")
                    {
                        // Todo: Get number of playoff weeks and set Semi-finals, Quarter-Finals, etc.
                        gameType = $"Playoffs";
                    }
                    else if (match.GameType == "p")
                    {
                        // Todo: if loser bracket, say so here.
                        gameType = $"Grand Finals";
                    }

                    foreach (var gameMap in match.GameMaps)
                    {
                        List<UpcomingImagesDto> images = new List<UpcomingImagesDto>();
                        foreach (var image in gameMap.FkIdMapNavigation.MapImages)
                        {
                            images.Add(new UpcomingImagesDto { 
                                ImagePath = image.FkIdImageFileNavigation.FileName,
                                ImageCaption = image.FkIdImageFileNavigation.Caption
                            });
                        }

                        maps.Add(new UpcomingMapsDto {
                            MapName = gameMap.FkIdMapNavigation.MapName,
                            MapPack = gameMap.FkIdMapNavigation.MapPack,
                            MapNumber = String.Format("MAP{0:00}", gameMap.FkIdMapNavigation.MapNumber),
                            MapImages = images
                        });
                    }

                    upcoming.Add(new UpcomingMatchesDto
                    {
                        BlueTeam = match.FkIdTeamBlue,
                        RedTeam = match.FkIdTeamRed,
                        BlueTeamName = match.FkIdTeamBlueNavigation.TeamName,
                        RedTeamName = match.FkIdTeamRedNavigation.TeamName,
                        Id = match.IdGame,
                        ScheduledTime = match.GameDatetime,
                        RedTeamRecord = redRecord,
                        BlueTeamRecord = blueRecord,
                        SeasonName = match.FkIdSeasonNavigation.SeasonName,
                        GameType = gameType,
                        Maps = maps
                    });
                }
            }

            return new UpcomingMatchesVm
            {
                UpcomingMatches = upcoming.OrderBy(o => o.ScheduledTime).ToList()
            };
        }
    }
}
