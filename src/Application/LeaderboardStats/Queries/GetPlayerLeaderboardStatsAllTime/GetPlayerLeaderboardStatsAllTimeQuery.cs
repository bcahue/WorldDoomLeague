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

namespace WorldDoomLeague.Application.LeaderboardStats.Queries.GetPlayerLeaderboardStatsAllTime
{
    public class GetPlayerLeaderboardStatsAllTimeQuery : IRequest<PlayerLeaderboardAllTimeStatsVm>
    {
        public LeaderboardStatsMode Mode { get; }

        public GetPlayerLeaderboardStatsAllTimeQuery(LeaderboardStatsMode mode)
        {
            Mode = mode; 
        }
    }

    public class GetPlayerLeaderboardStatsAllTimeQueryHandler : IRequestHandler<GetPlayerLeaderboardStatsAllTimeQuery, PlayerLeaderboardAllTimeStatsVm>
    {
        private readonly IApplicationDbContext _context;

        public GetPlayerLeaderboardStatsAllTimeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerLeaderboardAllTimeStatsVm> Handle(GetPlayerLeaderboardStatsAllTimeQuery request, CancellationToken cancellationToken)
        {
            var alltimeStatsRounds = await _context.StatsRounds
                    .Include(i => i.FkIdPlayerNavigation)
                    .Include(i => i.FkIdRoundNavigation)
                    .ToListAsync(cancellationToken);

            var playerStats = alltimeStatsRounds.GroupBy(g => g.FkIdPlayer).Where(w => w.Count() >= 5); // Players who have more than 5
                                                                                                        // stats rounds this season.

            List<LeaderboardStatsConfiguration> statCategories = new List<LeaderboardStatsConfiguration>();

            statCategories.Add(new LeaderboardStatsConfiguration(LeaderboardStatsType.Captures, request.Mode));
            statCategories.Add(new LeaderboardStatsConfiguration(LeaderboardStatsType.Damage, request.Mode));
            statCategories.Add(new LeaderboardStatsConfiguration(LeaderboardStatsType.FlagDefenses, request.Mode));
            statCategories.Add(new LeaderboardStatsConfiguration(LeaderboardStatsType.FlagTouches, request.Mode));
            statCategories.Add(new LeaderboardStatsConfiguration(LeaderboardStatsType.Frags, request.Mode));

            List<PlayerLeaderboardStatsDto> leaderboardStats = new List<PlayerLeaderboardStatsDto>();

            foreach (var stat in statCategories)
            {
                List<LeaderboardStatsDto> categoryTopStats = new List<LeaderboardStatsDto>();
                switch (stat.Type)
                {
                    case LeaderboardStatsType.Damage:
                        {
                            switch (stat.Mode)
                            {
                                case LeaderboardStatsMode.Per1Min:
                                    {
                                        var dmg = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                dmgSum = s.Sum(s => s.TotalDamage + s.TotalDamageFlagCarrier)
                                            })
                                            .OrderByDescending(o => (o.dmgSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var dmgStat in dmg)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = dmgStat.playerName,
                                                Stat = dmgStat.dmgSum / dmgStat.timeMinutes
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Per8Min:
                                    {
                                        var dmg = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                dmgSum = s.Sum(s => s.TotalDamage + s.TotalDamageFlagCarrier)
                                            })
                                            .OrderByDescending(o => (o.dmgSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var dmgStat in dmg)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = dmgStat.playerName,
                                                Stat = (dmgStat.dmgSum / dmgStat.timeMinutes) * 8
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Total:
                                    {
                                        var dmg = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                dmgSum = s.Sum(s => s.TotalDamage + s.TotalDamageFlagCarrier)
                                            })
                                            .OrderByDescending(o => o.dmgSum).Take(10);
                                        var i = 1;
                                        foreach (var dmgStat in dmg)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = dmgStat.playerName,
                                                Stat = dmgStat.dmgSum
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.PerRound:
                                    {
                                        var dmg = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                roundsPlayed = s.Select(s => s.IdStatsRound).Count(),
                                                dmgSum = s.Sum(s => s.TotalDamage + s.TotalDamageFlagCarrier)
                                            })
                                            .OrderByDescending(o => o.dmgSum / o.roundsPlayed).Take(10);
                                        var i = 1;
                                        foreach (var dmgStat in dmg)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = dmgStat.playerName,
                                                Stat = dmgStat.dmgSum / dmgStat.roundsPlayed
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case LeaderboardStatsType.Captures:
                        {
                            switch (stat.Mode)
                            {
                                case LeaderboardStatsMode.Per1Min:
                                    {
                                        var caps = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                captureSum = (double)s.Sum(s => s.TotalCaptures + s.TotalPickupCaptures)
                                            })
                                            .OrderByDescending(o => (o.captureSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var capsStat in caps)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = capsStat.playerName,
                                                Stat = capsStat.captureSum / capsStat.timeMinutes
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Per8Min:
                                    {
                                        var caps = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                captureSum = (double)s.Sum(s => s.TotalCaptures + s.TotalPickupCaptures)
                                            })
                                            .OrderByDescending(o => (o.captureSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var capsStat in caps)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = capsStat.playerName,
                                                Stat = (capsStat.captureSum / capsStat.timeMinutes) * 8.0
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.PerRound:
                                    {
                                        var caps = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                roundsPlayed = s.Select(s => s.IdStatsRound).Count(),
                                                captureSum = (double)s.Sum(s => s.TotalCaptures + s.TotalPickupCaptures)
                                            })
                                            .OrderByDescending(o => o.captureSum / o.roundsPlayed).Take(10);
                                        var i = 1;
                                        foreach (var capsStat in caps)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = capsStat.playerName,
                                                Stat = capsStat.captureSum / capsStat.roundsPlayed
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Total:
                                    {
                                        var caps = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                captureSum = s.Sum(s => s.TotalCaptures + s.TotalPickupCaptures)
                                            })
                                            .OrderByDescending(o => o.captureSum).Take(10);
                                        var i = 1;
                                        foreach (var capsStat in caps)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = capsStat.playerName,
                                                Stat = capsStat.captureSum
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case LeaderboardStatsType.FlagDefenses:
                        {
                            switch (stat.Mode)
                            {
                                case LeaderboardStatsMode.Per1Min:
                                    {
                                        var defenses = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                defensesSum = (double)s.Sum(s => s.TotalCarrierKills)
                                            })
                                            .OrderByDescending(o => (o.defensesSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var defensesStat in defenses)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = defensesStat.playerName,
                                                Stat = defensesStat.defensesSum / defensesStat.timeMinutes
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Per8Min:
                                    {
                                        var defenses = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                defensesSum = (double)s.Sum(s => s.TotalCarrierKills)
                                            })
                                            .OrderByDescending(o => (o.defensesSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var defensesStat in defenses)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = defensesStat.playerName,
                                                Stat = (defensesStat.defensesSum / defensesStat.timeMinutes) * 8
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.PerRound:
                                    {
                                        var defenses = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                roundsPlayed = s.Select(s => s.IdStatsRound).Count(),
                                                defensesSum = (double)s.Sum(s => s.TotalCarrierKills)
                                            })
                                            .OrderByDescending(o => (o.defensesSum / o.roundsPlayed)).Take(10);
                                        var i = 1;
                                        foreach (var defensesStat in defenses)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = defensesStat.playerName,
                                                Stat = defensesStat.defensesSum / defensesStat.roundsPlayed
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Total:
                                    {
                                        var defenses = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                defensesSum = s.Sum(s => s.TotalCarrierKills)
                                            })
                                            .OrderByDescending(o => o.defensesSum).Take(10);
                                        var i = 1;
                                        foreach (var defensesStat in defenses)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = defensesStat.playerName,
                                                Stat = defensesStat.defensesSum
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case LeaderboardStatsType.Frags:
                        {
                            switch (stat.Mode)
                            {
                                case LeaderboardStatsMode.Per1Min:
                                    {
                                        var frags = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                fragsSum = (double)s.Sum(s => (s.TotalKills + s.TotalCarrierKills))
                                            })
                                            .OrderByDescending(o => o.fragsSum / o.timeMinutes).Take(10);
                                        var i = 1;
                                        foreach (var fragsStat in frags)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = fragsStat.playerName,
                                                Stat = fragsStat.fragsSum / fragsStat.timeMinutes
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Per8Min:
                                    {
                                        var frags = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                fragsSum = (double)s.Sum(s => (s.TotalKills + s.TotalCarrierKills))
                                            })
                                            .OrderByDescending(o => o.fragsSum / o.timeMinutes).Take(10);
                                        var i = 1;
                                        foreach (var fragsStat in frags)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = fragsStat.playerName,
                                                Stat = (fragsStat.fragsSum / fragsStat.timeMinutes) * 8.0
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.PerRound:
                                    {
                                        var frags = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                roundsPlayed = s.Select(s => s.IdStatsRound).Count(),
                                                fragsSum = (double)s.Sum(s => s.TotalKills + s.TotalCarrierKills)
                                            })
                                            .OrderByDescending(o => (o.fragsSum / o.roundsPlayed)).Take(10);
                                        var i = 1;
                                        foreach (var fragsStat in frags)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = fragsStat.playerName,
                                                Stat = fragsStat.fragsSum / fragsStat.roundsPlayed
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Total:
                                    {
                                        var frags = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                fragsSum = s.Sum(s => (s.TotalKills + s.TotalCarrierKills))
                                            })
                                            .OrderByDescending(o => o.fragsSum).Take(10);
                                        var i = 1;
                                        foreach (var fragsStat in frags)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = fragsStat.playerName,
                                                Stat = fragsStat.fragsSum
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case LeaderboardStatsType.FlagTouches:
                        {
                            switch (stat.Mode)
                            {
                                case LeaderboardStatsMode.Per1Min:
                                    {
                                        var touches = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                touchesSum = (double)s.Sum(s => (s.TotalTouches + s.TotalPickupTouches))
                                            })
                                            .OrderByDescending(o => o.touchesSum / o.timeMinutes).Take(10);
                                        var i = 1;
                                        foreach (var touchesStat in touches)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = touchesStat.playerName,
                                                Stat = touchesStat.touchesSum / touchesStat.timeMinutes
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Per8Min:
                                    {
                                        var touches = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                timeMinutes = TimeSpan.FromSeconds((double)s.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
                                                    .TotalMinutes,
                                                touchesSum = (double)s.Sum(s => (s.TotalTouches + s.TotalPickupTouches))
                                            })
                                            .OrderByDescending(o => (o.touchesSum / o.timeMinutes)).Take(10);
                                        var i = 1;
                                        foreach (var touchesStat in touches)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = touchesStat.playerName,
                                                Stat = (touchesStat.touchesSum / touchesStat.timeMinutes) * 8.0
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.PerRound:
                                    {
                                        var touches = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                roundsPlayed = s.Select (s => s.IdStatsRound).Count(),
                                                touchesSum = (double)s.Sum(s => (s.TotalTouches + s.TotalPickupTouches))
                                            })
                                            .OrderByDescending(o => o.touchesSum / o.roundsPlayed).Take(10);
                                        var i = 1;
                                        foreach (var touchesStat in touches)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = touchesStat.playerName,
                                                Stat = touchesStat.touchesSum / touchesStat.roundsPlayed
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                                case LeaderboardStatsMode.Total:
                                    {
                                        var touches = playerStats
                                            .Select(s =>
                                            new
                                            {
                                                playerName = s.Select(s => s.FkIdPlayerNavigation.PlayerName).FirstOrDefault(),
                                                touchesSum = s.Sum(s => (s.TotalTouches + s.TotalPickupTouches))
                                            })
                                            .OrderByDescending(o => o.touchesSum).Take(10);
                                        var i = 1;
                                        foreach (var touchesStat in touches)
                                        {
                                            categoryTopStats.Add(new LeaderboardStatsDto
                                            {
                                                Id = i,
                                                PlayerName = touchesStat.playerName,
                                                Stat = touchesStat.touchesSum
                                            });
                                            i++;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                }
                leaderboardStats.Add(new PlayerLeaderboardStatsDto
                {
                    StatName = stat.ToString(),
                    LeaderboardStats = categoryTopStats
                });
            }

            return new PlayerLeaderboardAllTimeStatsVm
            {
                PlayerLeaderboardStats = leaderboardStats
            };
        }
    }
}
