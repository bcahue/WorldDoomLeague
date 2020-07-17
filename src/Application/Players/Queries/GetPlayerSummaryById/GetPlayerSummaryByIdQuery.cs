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
using System.Collections.Generic;
using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using WorldDoomLeague.Application.Common.Exceptions;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class GetPlayerSummaryByIdQuery : IRequest<PlayerSummaryVm>
    {
        public uint Id { get; }

        public GetPlayerSummaryByIdQuery(uint id)
        {
            Id = id;
        }
    }

    public class GetPlayerSummaryByIdQueryHandler : IRequestHandler<GetPlayerSummaryByIdQuery, PlayerSummaryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetPlayerSummaryByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerSummaryVm> Handle(GetPlayerSummaryByIdQuery request, CancellationToken cancellationToken)
        {
            // AutoMapper is nice when your Entities match your DTOs somewhat...
            // But when they don't, nothing beats the old fashioned DTO fill.
            // HOWEVER, this is just a query, so this is fine.
            // For ADDING objects into the system, ALWAYS use AutoMapper to send objects to the ORM,
            // and get the result in a DTO.

            // TODO
            // Make PlayerRecords tables to make this less ugly.

            PlayerSummaryVm playerSummary = new PlayerSummaryVm();

            List<SeasonsPlayedInDto> seasonsPlayedIn = new List<SeasonsPlayedInDto>();

            List<SeasonPlayerStatsDto> seasonStats   = new List<SeasonPlayerStatsDto>();

            PlayerCaptainRecordDto totalRecord = new PlayerCaptainRecordDto(new RoundGameRecordDto(new RecordDto(), new RecordDto()), new RoundGameRecordDto(new RecordDto(), new RecordDto()));
            PlayerCaptainRecordDto regularSeasonRecord = new PlayerCaptainRecordDto(new RoundGameRecordDto(new RecordDto(), new RecordDto()), new RoundGameRecordDto(new RecordDto(), new RecordDto()));
            PlayerCaptainRecordDto playoffRecord = new PlayerCaptainRecordDto(new RoundGameRecordDto(new RecordDto(), new RecordDto()), new RoundGameRecordDto(new RecordDto(), new RecordDto()));
            PlayerCaptainRecordDto finalsRecord = new PlayerCaptainRecordDto(new RoundGameRecordDto(new RecordDto(), new RecordDto()), new RoundGameRecordDto(new RecordDto(), new RecordDto()));

            // Get the id first
            var playerIdQuery = await _context.Player
                       .Where(p => p.Id == request.Id)
                       .FirstOrDefaultAsync();

            if (playerIdQuery != null)
            {
                playerSummary.Id   = playerIdQuery.Id;
                playerSummary.Name = playerIdQuery.PlayerName;
            }
            else
            {
                throw new NotFoundException(String.Format("The player {0} was not found in the system.", request.Id));
            }

            // Lets eager load enough data here to only call the database once.
            // We'll have a lot of logic performing on the data, but we want as much as we need from the database first.
            // We're sacrificing the page load time in exchange for not overloading the database with extra queries.
            // Using filtered include here to only get teams the player was drafted on.
            // EDIT: Not using filtered include here due to a bug. Bless this mess.
            // Once this is fixed, this can be extremely optimized.
            var teamsPlayedForQuery = from teams in _context.Teams
                                      join seasons in _context.Season
                                      on teams.FkIdSeason equals seasons.IdSeason
                                      where (teams.FkIdPlayerCaptain == request.Id ||
                                              teams.FkIdPlayerFirstpick == request.Id ||
                                              teams.FkIdPlayerSecondpick == request.Id ||
                                              teams.FkIdPlayerThirdpick == request.Id)
                                      select new
                                      {
                                          teams.IdTeam,
                                          teams.TeamName,
                                          seasons.SeasonName,
                                          seasons.IdSeason,
                                          teams.FkIdPlayerCaptain,
                                          teams.FkIdPlayerFirstpick,
                                          teams.FkIdPlayerSecondpick,
                                          teams.FkIdPlayerThirdpick,
                                      };

            // Find the seasons the player played in but wasn't drafted
            // either thru FA or trade.
            var playerSubbedForTeamQuery = from statsRounds in _context.StatsRounds
                                                 join subTeams in _context.Teams
                                                 on statsRounds.FkIdTeam equals subTeams.IdTeam
                                           where statsRounds.FkIdPlayer == request.Id &&
                                           (subTeams.FkIdPlayerCaptain != request.Id &&
                                           subTeams.FkIdPlayerFirstpick != request.Id &&
                                           subTeams.FkIdPlayerSecondpick != request.Id &&
                                           subTeams.FkIdPlayerThirdpick != request.Id)
                                           select new
                                           {
                                               statsRounds.FkIdTeam,
                                               subTeams.TeamName,
                                               statsRounds.FkIdSeason
                                           };


            // Find the seasons the player played in.
            var seasonsPlayedInQuery = from statRound in _context.StatsRounds
                                       join seasons in _context.Season
                                       on statRound.FkIdSeason equals seasons.IdSeason
                                       where statRound.FkIdPlayer == request.Id
                                       select new
                                       {
                                           statRound.FkIdSeason,
                                           seasons.SeasonName,
                                           seasons.FkIdTeamWinner
                                       };

            // Calculate which teams they played on.
            // Sub or official member.
            if (teamsPlayedForQuery.Any() || playerSubbedForTeamQuery.Any())
            {
                var teams = teamsPlayedForQuery.ToList();
                var seasons = seasonsPlayedInQuery.Distinct().ToList();
                var subs = playerSubbedForTeamQuery.Distinct().ToList();
                foreach (var season in seasons)
                {
                    List<TeamsPlayedForDto> teamsPlayedFor = new List<TeamsPlayedForDto>();
                    foreach (var team in teams)
                    {
                        if (team.IdSeason == season.FkIdSeason)
                        {
                            string draftPosition = "";
                            if (team.FkIdPlayerCaptain == request.Id)
                            {
                                draftPosition = "Captain";
                            }
                            else if (team.FkIdPlayerFirstpick == request.Id)
                            {
                                draftPosition = "First";
                            }
                            else if (team.FkIdPlayerSecondpick == request.Id)
                            {
                                draftPosition = "Second";
                            }
                            else if (team.FkIdPlayerThirdpick == request.Id)
                            {
                                draftPosition = "Third";
                            }
                            teamsPlayedFor.Add(new TeamsPlayedForDto(
                                team.IdTeam,
                                team.TeamName,
                                draftPosition,
                                season.FkIdTeamWinner == team.IdTeam));
                        }
                    }
                    foreach (var sub in subs)
                    {
                        if (sub.FkIdSeason == season.FkIdSeason)
                        {
                            teamsPlayedFor.Add(new TeamsPlayedForDto(
                                sub.FkIdTeam,
                                sub.TeamName,
                                "Free Agent/Sub/Traded after draft",
                                season.FkIdTeamWinner == sub.FkIdTeam));
                        }
                    }
                    seasonsPlayedIn.Add(new SeasonsPlayedInDto(season.FkIdSeason, season.SeasonName, teamsPlayedFor));
                }
            }

            //
            // This section builds the LifetimeStats section of the PlayerStatsDocument.
            //
            // Finally, get all time stats along with season stats.
            var allTimeStatsQuery = from statsRounds in _context.StatsRounds
                                    join players in _context.Player
                                    on statsRounds.FkIdPlayer equals players.Id
                                    where statsRounds.FkIdPlayer == request.Id
                                    select new
                                    {
                                        statsRounds.TotalKills,
                                        statsRounds.TotalCarrierKills,
                                        statsRounds.TotalDeaths,
                                        statsRounds.TotalEnvironmentDeaths,
                                        statsRounds.TotalDamage,
                                        statsRounds.TotalDamageFlagCarrier,
                                        statsRounds.TotalDamageWithFlag,
                                        statsRounds.TotalTouches,
                                        statsRounds.TotalPickupTouches,
                                        statsRounds.TotalAssists,
                                        statsRounds.TotalCaptures,
                                        statsRounds.TotalPickupCaptures,
                                        statsRounds.TotalFlagReturns,
                                        statsRounds.TotalPowerPickups,
                                        statsRounds.SpreeKillingSprees,
                                        statsRounds.SpreeRampage,
                                        statsRounds.SpreeDominations,
                                        statsRounds.SpreeUnstoppables,
                                        statsRounds.SpreeGodlikes,
                                        statsRounds.SpreeWickedsicks,
                                        statsRounds.MultiDoubleKills,
                                        statsRounds.MultiMultiKills,
                                        statsRounds.MultiUltraKills,
                                        statsRounds.MultiMonsterKills,
                                        statsRounds.FkIdSeason
                                    };

            // Get total rounds played
            var roundsPlayed = from rounds in _context.Rounds
                               join roundPlayers in _context.RoundPlayers
                               on rounds.IdRound equals roundPlayers.FkIdRound
                               where roundPlayers.FkIdPlayer == request.Id
                               select new
                               {
                                   rounds.RoundTicsDuration,
                                   rounds.FkIdSeason
                               };

            //context.RoundPlayers.Where(
            //   p => p.FkIdPlayer == playerId);

            // Get all stat rounds with the player to calculate lifetime win/loss/tie
            var lifetimeStatsRoundsPlayed = await _context.StatsRounds
                .Where(w => w.FkIdPlayer == request.Id)
                .ToListAsync();

            // Get all games to calculate win/loss
            var gamesPlayed = await _context.Games.ToListAsync();

            // Get rounds joined by games to determine round winners (todo: resolve missing fk on rounds table for wining team id. It can save a join.)
            var totalRounds = from games in _context.Games
                              join rounds in _context.Rounds
                              on games.IdGame equals rounds.FkIdGame
                              join roundPlayers in _context.RoundPlayers
                              on rounds.IdRound equals roundPlayers.FkIdRound
                              where roundPlayers.FkIdPlayer == request.Id
                              select new
                              {
                                  rounds.RoundWinner,
                                  games.FkIdTeamBlue,
                                  games.FkIdTeamRed,
                                  games.GameType,
                                  roundPlayers.FkIdPlayer,
                                  games.IdGame
                              };

            var allRoundPlayers = await _context.RoundPlayers.ToListAsync();

            // Get lifetime records
            foreach (var season in seasonsPlayedIn)
            {
                foreach (var team in season.TeamsPlayedFor)
                {
                    // If the player plays in 1 stat round, award them the win/loss/tie
                    var gameIdForGamesPlayedWhileOnTeam = lifetimeStatsRoundsPlayed.Where(
                        e => e.FkIdTeam == team.Id)
                        .Select(s => s.FkIdGame)
                        .Distinct();

                    // Get lifetime game stats
                    foreach (var game in gameIdForGamesPlayedWhileOnTeam)
                    {
                        totalRecord.Total.Matches.Wins += gamesPlayed
                            .Where(w => w.FkIdTeamWinner == team.Id && w.IdGame == game)
                            .Count();

                        totalRecord.Total.Matches.Losses += gamesPlayed
                            .Where(w =>
                            w.FkIdTeamWinner != team.Id &&
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.IdGame == game)
                            .Count();

                        totalRecord.Total.Matches.Ties += gamesPlayed
                            .Where(w =>
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.TeamWinnerColor == "t"
                            && w.IdGame == game)
                            .Count();

                        regularSeasonRecord.Total.Matches.Wins += gamesPlayed
                            .Where(w => w.FkIdTeamWinner == team.Id
                            && w.IdGame == game
                            && w.GameType == "n")
                            .Count();

                        regularSeasonRecord.Total.Matches.Losses += gamesPlayed
                            .Where(w =>
                            w.FkIdTeamWinner != team.Id &&
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.IdGame == game
                            && w.GameType == "n")
                            .Count();

                        regularSeasonRecord.Total.Matches.Ties += gamesPlayed
                            .Where(w =>
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.TeamWinnerColor == "t"
                            && w.IdGame == game
                            && w.GameType == "n")
                            .Count();

                        playoffRecord.Total.Matches.Wins += gamesPlayed
                            .Where(w => w.FkIdTeamWinner == team.Id
                            && w.IdGame == game
                            && w.GameType == "p")
                            .Count();

                        playoffRecord.Total.Matches.Losses += gamesPlayed
                            .Where(w =>
                            w.FkIdTeamWinner != team.Id &&
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.IdGame == game
                            && w.GameType == "p")
                            .Count();

                        playoffRecord.Total.Matches.Ties += gamesPlayed
                            .Where(w =>
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.TeamWinnerColor == "t"
                            && w.IdGame == game
                            && w.GameType == "p")
                            .Count();

                        finalsRecord.Total.Matches.Wins += gamesPlayed
                            .Where(w => w.FkIdTeamWinner == team.Id
                            && w.IdGame == game
                            && w.GameType == "f")
                            .Count();

                        finalsRecord.Total.Matches.Losses += gamesPlayed
                            .Where(w =>
                            w.FkIdTeamWinner != team.Id &&
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.IdGame == game
                            && w.GameType == "f")
                            .Count();

                        finalsRecord.Total.Matches.Ties += gamesPlayed
                            .Where(w =>
                            (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                            && w.TeamWinnerColor == "t"
                            && w.IdGame == game
                            && w.GameType == "f")
                            .Count();

                        // ez hack to get lifetime captain game records
                        // nvm not so ez :(
                        if (team.DraftPosition == "Captain")
                        {
                            totalRecord.AsCaptain.Matches.Wins += gamesPlayed
                                .Where(w => w.FkIdTeamWinner == team.Id && w.IdGame == game)
                                .Count();

                            totalRecord.AsCaptain.Matches.Losses += gamesPlayed
                                .Where(w =>
                                w.FkIdTeamWinner != team.Id &&
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.IdGame == game)
                                .Count();

                            totalRecord.AsCaptain.Matches.Ties += gamesPlayed
                                .Where(w =>
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.TeamWinnerColor == "t"
                                && w.IdGame == game)
                                .Count();

                            regularSeasonRecord.AsCaptain.Matches.Wins += gamesPlayed
                                .Where(w => w.FkIdTeamWinner == team.Id
                                && w.IdGame == game
                                && w.GameType == "n")
                                .Count();

                            regularSeasonRecord.AsCaptain.Matches.Losses += gamesPlayed
                                .Where(w =>
                                w.FkIdTeamWinner != team.Id &&
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.IdGame == game
                                && w.GameType == "n")
                                .Count();

                            regularSeasonRecord.AsCaptain.Matches.Ties += gamesPlayed
                                .Where(w =>
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.TeamWinnerColor == "t"
                                && w.IdGame == game
                                && w.GameType == "n")
                                .Count();

                            playoffRecord.AsCaptain.Matches.Wins += gamesPlayed
                                .Where(w => w.FkIdTeamWinner == team.Id
                                && w.IdGame == game
                                && w.GameType == "p")
                                .Count();

                            playoffRecord.AsCaptain.Matches.Losses += gamesPlayed
                                .Where(w =>
                                w.FkIdTeamWinner != team.Id &&
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.IdGame == game
                                && w.GameType == "p")
                                .Count();

                            playoffRecord.AsCaptain.Matches.Ties += gamesPlayed
                                .Where(w =>
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.TeamWinnerColor == "t"
                                && w.IdGame == game
                                && w.GameType == "p")
                                .Count();

                            finalsRecord.AsCaptain.Matches.Wins += gamesPlayed
                                .Where(w => w.FkIdTeamWinner == team.Id
                                && w.IdGame == game
                                && w.GameType == "f")
                                .Count();

                            finalsRecord.AsCaptain.Matches.Losses += gamesPlayed
                                .Where(w =>
                                w.FkIdTeamWinner != team.Id &&
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.IdGame == game
                                && w.GameType == "f")
                                .Count();

                            finalsRecord.AsCaptain.Matches.Ties += gamesPlayed
                                .Where(w =>
                                (w.FkIdTeamBlue == team.Id || w.FkIdTeamRed == team.Id)
                                && w.TeamWinnerColor == "t"
                                && w.IdGame == game
                                && w.GameType == "f")
                                .Count();
                        }
                    }

                    // Get lifetime total, regular season, playoff and finals round records now.
                    var roundsPlayedWhileOnTeam = totalRounds
                        .Where(w =>
                        (w.FkIdTeamBlue == team.Id ||
                        w.FkIdTeamRed == team.Id) &&
                        gameIdForGamesPlayedWhileOnTeam.Contains(w.IdGame));

                    totalRecord.Total.Rounds.Wins += roundsPlayedWhileOnTeam
                        .Where(e => (e.RoundWinner == "r"
                       && e.FkIdTeamRed == team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue == team.Id && e.FkIdPlayer == request.Id))
                        .Count();

                    totalRecord.Total.Rounds.Losses += roundsPlayedWhileOnTeam
                        .Where(e => (e.RoundWinner == "r"
                       && e.FkIdTeamRed != team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue != team.Id && e.FkIdPlayer == request.Id))
                        .Count();

                    totalRecord.Total.Rounds.Ties += roundsPlayedWhileOnTeam
                        .Where(e => e.RoundWinner == "t"
                        && e.FkIdPlayer == request.Id)
                        .Count();

                    regularSeasonRecord.Total.Rounds.Wins += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "n"
                       && ((e.RoundWinner == "r"
                       && e.FkIdTeamRed == team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue == team.Id)))
                        .Count();

                    regularSeasonRecord.Total.Rounds.Losses += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "n"
                       && ((e.RoundWinner == "r"
                       && e.FkIdTeamRed != team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue != team.Id)))
                        .Count();

                    regularSeasonRecord.Total.Rounds.Ties += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "n"
                       && e.RoundWinner == "t")
                        .Count();

                    playoffRecord.Total.Rounds.Wins += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "p"
                       && ((e.RoundWinner == "r"
                       && e.FkIdTeamRed == team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue == team.Id)))
                        .Count();

                    playoffRecord.Total.Rounds.Losses += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "p"
                       && ((e.RoundWinner == "r"
                       && e.FkIdTeamRed != team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue != team.Id)))
                        .Count();

                    playoffRecord.Total.Rounds.Ties += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "p"
                       && e.RoundWinner == "t")
                        .Count();

                    finalsRecord.Total.Rounds.Wins += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "f"
                       && ((e.RoundWinner == "r"
                       && e.FkIdTeamRed == team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue == team.Id)))
                        .Count();

                    finalsRecord.Total.Rounds.Losses += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "f"
                       && ((e.RoundWinner == "r"
                       && e.FkIdTeamRed != team.Id) ||
                       (e.RoundWinner == "b"
                       && e.FkIdTeamBlue != team.Id)))
                        .Count();

                    finalsRecord.Total.Rounds.Ties += roundsPlayedWhileOnTeam
                        .Where(e => e.GameType == "f"
                       && e.RoundWinner == "t")
                        .Count();


                    if (team.DraftPosition == "Captain")
                    {
                        totalRecord.AsCaptain.Rounds.Wins += roundsPlayedWhileOnTeam
                            .Where(e => (e.RoundWinner == "r"
                           && e.FkIdTeamRed == team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue == team.Id))
                            .Count();

                        totalRecord.AsCaptain.Rounds.Losses += roundsPlayedWhileOnTeam
                            .Where(e => (e.RoundWinner == "r"
                           && e.FkIdTeamRed != team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue != team.Id))
                            .Count();

                        totalRecord.AsCaptain.Rounds.Ties += roundsPlayedWhileOnTeam
                            .Where(e => e.RoundWinner == "t")
                            .Count();

                        regularSeasonRecord.AsCaptain.Rounds.Wins += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "n"
                           && ((e.RoundWinner == "r"
                           && e.FkIdTeamRed == team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue == team.Id)))
                            .Count();

                        regularSeasonRecord.AsCaptain.Rounds.Losses += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "n"
                           && ((e.RoundWinner == "r"
                           && e.FkIdTeamRed != team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue != team.Id)))
                            .Count();

                        regularSeasonRecord.AsCaptain.Rounds.Ties += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "n"
                           && e.RoundWinner == "t")
                            .Count();

                        playoffRecord.AsCaptain.Rounds.Wins += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "p"
                           && ((e.RoundWinner == "r"
                           && e.FkIdTeamRed == team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue == team.Id)))
                            .Count();

                        playoffRecord.AsCaptain.Rounds.Losses += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "p"
                           && ((e.RoundWinner == "r"
                           && e.FkIdTeamRed != team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue != team.Id)))
                            .Count();

                        playoffRecord.AsCaptain.Rounds.Ties += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "p"
                           && e.RoundWinner == "t")
                            .Count();

                        finalsRecord.AsCaptain.Rounds.Wins += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "f"
                           && ((e.RoundWinner == "r"
                           && e.FkIdTeamRed == team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue == team.Id)))
                            .Count();

                        finalsRecord.AsCaptain.Rounds.Losses += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "f"
                           && ((e.RoundWinner == "r"
                           && e.FkIdTeamRed != team.Id) ||
                           (e.RoundWinner == "b"
                           && e.FkIdTeamBlue != team.Id)))
                            .Count();

                        finalsRecord.AsCaptain.Rounds.Ties += roundsPlayedWhileOnTeam
                            .Where(e => e.GameType == "f"
                           && e.RoundWinner == "t")
                            .Count();
                    }
                }
            }

            if (allTimeStatsQuery.Any())
            {
                playerSummary.PlayerAllTimeStats = new StatsDto(
                    roundsPlayed.Count(),
                    TimeSpan.FromSeconds(roundsPlayed.Select(e => (int)e.RoundTicsDuration).Sum() / 35), // 35 tics in a second...
                    allTimeStatsQuery.Sum(a => a.TotalKills) + allTimeStatsQuery.Sum(a => a.TotalCarrierKills),
                    allTimeStatsQuery.Sum(a => a.TotalKills),
                    allTimeStatsQuery.Sum(a => a.TotalCarrierKills),
                    allTimeStatsQuery.Sum(a => a.TotalDeaths),
                    allTimeStatsQuery.Sum(a => a.TotalEnvironmentDeaths),
                    allTimeStatsQuery.Sum(a => a.TotalDamage),
                    allTimeStatsQuery.Sum(a => a.TotalDamageFlagCarrier),
                    allTimeStatsQuery.Sum(a => a.TotalDamageWithFlag),
                    allTimeStatsQuery.Sum(a => a.TotalTouches),
                    allTimeStatsQuery.Sum(a => a.TotalPickupTouches),
                    allTimeStatsQuery.Sum(a => a.TotalAssists),
                    allTimeStatsQuery.Sum(a => a.TotalCaptures),
                    allTimeStatsQuery.Sum(a => a.TotalPickupCaptures),
                    allTimeStatsQuery.Sum(a => a.TotalFlagReturns),
                    allTimeStatsQuery.Sum(a => a.TotalPowerPickups),
                    allTimeStatsQuery.Sum(a => a.SpreeKillingSprees),
                    allTimeStatsQuery.Sum(a => a.SpreeRampage),
                    allTimeStatsQuery.Sum(a => a.SpreeDominations),
                    allTimeStatsQuery.Sum(a => a.SpreeUnstoppables),
                    allTimeStatsQuery.Sum(a => a.SpreeGodlikes),
                    allTimeStatsQuery.Sum(a => a.SpreeWickedsicks),
                    allTimeStatsQuery.Sum(a => a.MultiDoubleKills),
                    allTimeStatsQuery.Sum(a => a.MultiMultiKills),
                    allTimeStatsQuery.Sum(a => a.MultiUltraKills),
                    allTimeStatsQuery.Sum(a => a.MultiMonsterKills)
                    );
            }


            foreach (var node in seasonsPlayedIn)
            {
                var seasonStatsQuery = allTimeStatsQuery.Where(s => s.FkIdSeason == node.Id);

                var seasonRoundsPlayed = roundsPlayed.Where(e => e.FkIdSeason == node.Id);

                seasonStats.Add(new SeasonPlayerStatsDto(node.Id, node.SeasonName, new StatsDto(
                    seasonRoundsPlayed.Count(),
                    TimeSpan.FromSeconds(seasonRoundsPlayed.Select(e => (int)e.RoundTicsDuration).Sum() / 35), // 35 tics in a second...
                    seasonStatsQuery.Sum(a => a.TotalKills) + seasonStatsQuery.Sum(a => a.TotalCarrierKills),
                    seasonStatsQuery.Sum(a => a.TotalKills),
                    seasonStatsQuery.Sum(a => a.TotalCarrierKills),
                    seasonStatsQuery.Sum(a => a.TotalDeaths),
                    seasonStatsQuery.Sum(a => a.TotalEnvironmentDeaths),
                    seasonStatsQuery.Sum(a => a.TotalDamage),
                    seasonStatsQuery.Sum(a => a.TotalDamageFlagCarrier),
                    seasonStatsQuery.Sum(a => a.TotalDamageWithFlag),
                    seasonStatsQuery.Sum(a => a.TotalTouches),
                    seasonStatsQuery.Sum(a => a.TotalPickupTouches),
                    seasonStatsQuery.Sum(a => a.TotalAssists),
                    seasonStatsQuery.Sum(a => a.TotalCaptures),
                    seasonStatsQuery.Sum(a => a.TotalPickupCaptures),
                    seasonStatsQuery.Sum(a => a.TotalFlagReturns),
                    seasonStatsQuery.Sum(a => a.TotalPowerPickups),
                    seasonStatsQuery.Sum(a => a.SpreeKillingSprees),
                    seasonStatsQuery.Sum(a => a.SpreeRampage),
                    seasonStatsQuery.Sum(a => a.SpreeDominations),
                    seasonStatsQuery.Sum(a => a.SpreeUnstoppables),
                    seasonStatsQuery.Sum(a => a.SpreeGodlikes),
                    seasonStatsQuery.Sum(a => a.SpreeWickedsicks),
                    seasonStatsQuery.Sum(a => a.MultiDoubleKills),
                    seasonStatsQuery.Sum(a => a.MultiMultiKills),
                    seasonStatsQuery.Sum(a => a.MultiUltraKills),
                    seasonStatsQuery.Sum(a => a.MultiMonsterKills)
                    )));
            }

            playerSummary.SeasonsPlayedIn = seasonsPlayedIn;

            playerSummary.TotalRecord = totalRecord;
            playerSummary.RegularSeasonRecord = regularSeasonRecord;
            playerSummary.PlayoffRecord = playoffRecord;
            playerSummary.FinalsRecord = finalsRecord;

            playerSummary.SeasonStats = seasonStats;

            return playerSummary;
        }
    }
}
