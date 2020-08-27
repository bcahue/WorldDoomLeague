using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Application.Common.Interfaces;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamSummaryByTeamId
{
    public class GetTeamSummaryByTeamIdQuery : IRequest<TeamSummaryVm>
    {
        public uint TeamId { get; }

        public GetTeamSummaryByTeamIdQuery(uint teamId)
        {
            TeamId = teamId;
        }
    }

    public class GetTeamSummaryByTeamIdQueryHandler : IRequestHandler<GetTeamSummaryByTeamIdQuery, TeamSummaryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetTeamSummaryByTeamIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeamSummaryVm> Handle(GetTeamSummaryByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var teamInfo = await _context.Teams
                    .Where(w => w.IdTeam == request.TeamId)
                    .Include(i => i.FkIdPlayerCaptainNavigation)
                    .Include(i => i.FkIdPlayerFirstpickNavigation)
                    .Include(i => i.FkIdPlayerSecondpickNavigation)
                    .Include(i => i.FkIdPlayerThirdpickNavigation)
                    .FirstOrDefaultAsync(cancellationToken)
                    ??
                    throw new NotFoundException(string.Format("Team id {0} was not found in the database.", request.TeamId));

            var seasonTeamStats = await _context.StatsRounds
                    .Include(i => i.FkIdPlayerNavigation)
                    .Include(i => i.FkIdGameNavigation)
                        .ThenInclude(ti => ti.FkIdTeamBlueNavigation)
                    .Include(i => i.FkIdGameNavigation)
                        .ThenInclude(ti => ti.FkIdTeamRedNavigation)
                    .Include(i => i.FkIdSeasonNavigation)
                    .Include(i => i.FkIdRoundNavigation)
                    .Include(i => i.FkIdMapNavigation)
                    .Where(w => w.FkIdTeam == request.TeamId)
                    .ToListAsync(cancellationToken);

            List<TeamDraftDto> teamDraftList = new List<TeamDraftDto>();

            List<TeamPlayerDto> playerList = new List<TeamPlayerDto>();

            // Create the draft list.
            teamDraftList.Add(new TeamDraftDto() 
            { 
                PlayerId = teamInfo.FkIdPlayerCaptainNavigation.Id,
                PlayerName = teamInfo.FkIdPlayerCaptainNavigation.PlayerName,
                DraftPosition = "Captain"
            });
            teamDraftList.Add(new TeamDraftDto()
            {
                PlayerId = teamInfo.FkIdPlayerFirstpickNavigation.Id,
                PlayerName = teamInfo.FkIdPlayerFirstpickNavigation.PlayerName,
                DraftPosition = "First Pick"
            });
            teamDraftList.Add(new TeamDraftDto()
            {
                PlayerId = teamInfo.FkIdPlayerSecondpickNavigation.Id,
                PlayerName = teamInfo.FkIdPlayerSecondpickNavigation.PlayerName,
                DraftPosition = "Second Pick"
            });
            teamDraftList.Add(new TeamDraftDto()
            {
                PlayerId = teamInfo.FkIdPlayerThirdpickNavigation.Id,
                PlayerName = teamInfo.FkIdPlayerThirdpickNavigation.PlayerName,
                DraftPosition = "Third Pick"
            });

            // Get all players that played for the team.
            var gamePlayers = seasonTeamStats
                .Select(s => s.FkIdPlayerNavigation).Distinct();

            foreach (var player in gamePlayers)
            {
                playerList.Add(new TeamPlayerDto() { PlayerId = player.Id, PlayerName = player.PlayerName });
            }

            List<GamesPlayedDto> gamesList = new List<GamesPlayedDto>();

            // Get all the games the team played.
            var gamesPlayed = seasonTeamStats.Select(s => s.FkIdGameNavigation).Distinct().ToList();

            foreach (var game in gamesPlayed)
            {
                string gameName = string.Format("{0} vs. {1}", game.FkIdTeamBlueNavigation.TeamName, game.FkIdTeamRedNavigation.TeamName);
                gamesList.Add(new GamesPlayedDto() { GameId = game.IdGame, GameName = gameName });
            }

            // Fill in the team stats.
            TeamStatsDto teamStats = new TeamStatsDto()
            {
                // Assists simply track players who were apart of a flag capture (who didn't actually capture it.)
                Assists                     = seasonTeamStats.Sum(s => s.TotalAssists),
                // Captures and stats relating to captures.
                Points                      = seasonTeamStats.Sum(s => (s.TotalCaptures + s.TotalPickupCaptures)),
                Captures                    = seasonTeamStats.Sum(s => s.TotalCaptures),
                PickupCaptures              = seasonTeamStats.Sum(s => s.TotalPickupCaptures),
                CaptureBlueArmorAverage     = seasonTeamStats.Where(w => w.CaptureBlueArmorAverage > 0).Average(a => a.CaptureBlueArmorAverage),
                CaptureBlueArmorMax         = seasonTeamStats.Where(w => w.CaptureBlueArmorMax > 0).Max(a => a.CaptureBlueArmorMax),
                CaptureBlueArmorMin         = seasonTeamStats.Where(w => w.CaptureBlueArmorMin > 0).Min(a => a.CaptureBlueArmorMin),
                CaptureGreenArmorAverage    = seasonTeamStats.Where(w => w.CaptureGreenArmorAverage > 0).Average(a => a.CaptureGreenArmorAverage),
                CaptureGreenArmorMax        = seasonTeamStats.Where(w => w.CaptureGreenArmorMax > 0).Max(a => a.CaptureGreenArmorMax),
                CaptureGreenArmorMin        = seasonTeamStats.Where(w => w.CaptureGreenArmorMin > 0).Min(a => a.CaptureGreenArmorMin),
                CaptureHealthAverage        = seasonTeamStats.Where(w => w.CaptureHealthAverage > 0).Average(a => a.CaptureHealthAverage),
                CaptureHealthMax            = seasonTeamStats.Where(w => w.CaptureHealthMax > 0).Max(a => a.CaptureHealthMax),
                CaptureHealthMin            = seasonTeamStats.Where(w => w.CaptureHealthMin > 0).Min(a => a.CaptureHealthMin),
                CapturesWithSuperPickups    = seasonTeamStats.Sum(s => s.CaptureWithSuperPickups),
                HighestKillsBeforeCapturing = seasonTeamStats.Sum(s => s.HighestKillsBeforeCapturing),
                CaptureTimeAverage          = TimeSpan.FromSeconds(seasonTeamStats.Where(w => w.CaptureTicsAverage > 0).Average(s => s.CaptureTicsAverage ?? 0.0) / 35),
                CaptureTimeMin              = TimeSpan.FromSeconds(seasonTeamStats.Where(w => w.CaptureTicsMin > 0).Min(s => s.CaptureTicsMin ?? 0.0) / 35),
                CaptureTimeMax              = TimeSpan.FromSeconds(seasonTeamStats.Where(w => w.CaptureTicsMax > 0).Max(s => s.CaptureTicsMax ?? 0.0) / 35),
                PickupCaptureTimeAverage    = TimeSpan.FromSeconds(seasonTeamStats.Where(w => w.PickupCaptureTicsAverage > 0).Average(s => s.PickupCaptureTicsAverage ?? 0.0) / 35),
                PickupCaptureTimeMin        = TimeSpan.FromSeconds(seasonTeamStats.Where(w => w.PickupCaptureTicsMin > 0).Min(s => s.PickupCaptureTicsMin ?? 0.0) / 35),
                PickupCaptureTimeMax        = TimeSpan.FromSeconds(seasonTeamStats.Where(w => w.PickupCaptureTicsMax > 0).Max(s => s.PickupCaptureTicsMax ?? 0.0) / 35),
                // Damage and stats related to damage.
                Damage                              = seasonTeamStats.Sum(s => (s.TotalDamage + s.TotalDamageFlagCarrier)),
                DamageBetweenTouchAndCaptureAverage = seasonTeamStats.Where(w => w.DamageOutputBetweenTouchCaptureAverage > 0).Average(s => s.DamageOutputBetweenTouchCaptureAverage),
                DamageBetweenTouchAndCaptureMax     = seasonTeamStats.Where(w => w.DamageOutputBetweenTouchCaptureMax > 0).Max(s => s.DamageOutputBetweenTouchCaptureMax),
                //DamageBetweenTouchAndCaptureMin         = seasonTeamStats.Where(w => w.DamageOutputBetweenTouchCaptureMin > 0)
                //                                            .Max(s => s.DamageOutputBetweenTouchCaptureMin),
                DamageDoneWithFlag                      = seasonTeamStats.Sum(s => s.TotalDamageWithFlag),
                DamageTakenFromEnvironment              = seasonTeamStats.Sum(s => s.TotalDamageTakenEnvironment),
                DamageTakenFromEnvironmentAsFlagCarrier = seasonTeamStats.Sum(s => s.TotalDamageCarrierTakenEnvironment),
                DamageToBlueArmor                       = seasonTeamStats.Sum(s => s.TotalDamageBlueArmor),
                DamageToFlagCarriers                    = seasonTeamStats.Sum(s => s.TotalDamageFlagCarrier),
                DamageToGreenArmor                      = seasonTeamStats.Sum(s => s.TotalDamageGreenArmor),
                // Deaths and stats related to deaths
                Deaths                          = seasonTeamStats.Sum(s => s.TotalDeaths),
                EnvironmentalDeaths             = seasonTeamStats.Sum(s => s.TotalEnvironmentDeaths),
                EnvironmentalFlagCarrierDeaths  = seasonTeamStats.Sum(s => s.TotalEnvironmentCarrierDeaths),
                // Flag defenses and related stats.
                FlagDefenses                                        = seasonTeamStats.Sum(s => s.TotalCarrierKills),
                // Flag returns and related stats.
                FlagReturns                                         = seasonTeamStats.Sum(s => s.TotalFlagReturns),
                // Flag touches and related stats.
                FlagTouches                                         = seasonTeamStats.Sum(s => s.TotalTouches),
                BlueArmorWhenTouchingFlagAverage                    = seasonTeamStats.Where(w => w.TouchBlueArmorAverage > 0).Average(a => a.TouchBlueArmorAverage),
                BlueArmorWhenTouchingFlagMax                        = seasonTeamStats.Where(w => w.TouchBlueArmorMax > 0).Max(a => a.TouchBlueArmorMax),
                BlueArmorWhenTouchingFlagMin                        = seasonTeamStats.Where(w => w.TouchBlueArmorMin > 0).Min(a => a.TouchBlueArmorMin),
                GreenArmorWhenTouchingFlagAverage                   = seasonTeamStats.Where(w => w.TouchGreenArmorAverage > 0).Average(a => a.TouchGreenArmorAverage),
                GreenArmorWhenTouchingFlagMax                       = seasonTeamStats.Where(w => w.TouchGreenArmorMax > 0).Max(a => a.TouchGreenArmorMax),
                GreenArmorWhenTouchingFlagMin                       = seasonTeamStats.Where(w => w.TouchGreenArmorMin > 0).Min(a => a.TouchGreenArmorMin),
                HealthWhenTouchingFlagAverage                       = seasonTeamStats.Where(w => w.TouchHealthAverage > 0).Average(a => a.TouchHealthAverage),
                HealthWhenTouchingFlagMax                           = seasonTeamStats.Where(w => w.TouchHealthMax > 0).Max(a => a.TouchHealthMax),
                HealthWhenTouchingFlagMin                           = seasonTeamStats.Where(w => w.TouchHealthMin > 0).Min(a => a.TouchHealthMin),
                HealthWhenTouchingFlagThatResultsInCaptureAverage   = seasonTeamStats.Where(w => w.TouchHealthResultCaptureAverage > 0).Average(a => a.TouchHealthResultCaptureAverage),
                HealthWhenTouchingFlagThatResultsInCaptureMax       = seasonTeamStats.Where(w => w.TouchHealthResultCaptureMax > 0).Max(a => a.TouchHealthResultCaptureMax),
                HealthWhenTouchingFlagThatResultsInCaptureMin       = seasonTeamStats.Where(w => w.TouchHealthResultCaptureMin > 0).Min(a => a.TouchHealthResultCaptureMin),
                PickupFlagTouches                                   = seasonTeamStats.Sum(s => s.TotalPickupTouches),
                // Frags and frag stats
                Frags                               = seasonTeamStats.Sum(s => (s.TotalKills + s.TotalCarrierKills)),
                FlagCarriersKilledWhileHoldingFlag  = seasonTeamStats.Sum(s => s.CarriersKilledWhileHoldingFlag),
                // Pickups
                Powerups                = seasonTeamStats.Sum(s => s.TotalPowerPickups),
                PickupHealthGained      = seasonTeamStats.Sum(s => s.PickupHealthGained),
                HealthFromNonPowerups   = seasonTeamStats.Sum(s => s.HealthFromNonpowerPickups),
                // Sprees
                LongestSpree    = seasonTeamStats.Max(s => s.LongestSpree),
                // Multi Kills
                HighestMultiKill    = seasonTeamStats.Max(s => s.HighestMultiFrags),
                // Metadata (used to find modes, like per minute stats, per round stats, etc.)
                GamesPlayed         = seasonTeamStats.Select(c => c.FkIdGameNavigation).Distinct().Count(),
                RoundsPlayed        = seasonTeamStats.Select(c => c.FkIdRoundNavigation).Distinct().Count(),
                TimePlayed          = TimeSpan.FromSeconds((double)seasonTeamStats.Sum(s => s.FkIdRoundNavigation.RoundTicsDuration) / 35)
            };

            return new TeamSummaryVm
            {
                TeamId           = request.TeamId,
                TeamName         = teamInfo.TeamName,
                SeasonId         = teamInfo.FkIdSeason,
                SeasonName       = teamInfo.FkIdSeasonNavigation.SeasonName,
                DidTeamWinSeason = teamInfo.FkIdSeasonNavigation.FkIdTeamWinner == teamInfo.IdTeam,
                Draft            = teamDraftList,
                GamesPlayed      = gamesList,
                Players          = playerList,
                Stats            = teamStats
            };
        }
    }
}
