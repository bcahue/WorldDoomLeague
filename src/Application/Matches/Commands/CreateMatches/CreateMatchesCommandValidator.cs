using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatches
{
    public class CreateMatchesCommandValidator : AbstractValidator<CreateMatchesCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMatchesCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.SeasonId)
                .NotEmpty().WithMessage("SeasonId is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season id {PropertyValue} does not exist.")
                .MustAsync(BeValidWeeks).WithMessage("Week detected in the request that does not exist in the specified season id {PropertyValue}.")
                .MustAsync(TeamsExistWithinSeason).WithMessage("The specified season id {PropertyValue} does not contain team ids listed in the GameList.")
                .MustAsync(WeeksDontOutlastRegularSeasonWeeks).WithMessage("The amount of weeks specified in the request does not match the amount of the regular season weeks in the database.")
                .MustAsync(HaveAtLeastOneGamePerTeam).WithMessage("The specified WeeklyGames has a team with no games in a regular season week.");

            RuleFor(v => v.WeeklyGames)
                .Must(HaveSameAmountGamesPerWeek).WithMessage("The specified WeeklyGames has an unequal amount of games for each week.");

            RuleForEach(v => v.WeeklyGames)
                .SetValidator(new WeeklyGamesValidator(context));
        }

        public async Task<bool> BeValidSeason(uint season, CancellationToken cancellationToken)
        {
            return await _context.Season
                .AnyAsync(p => p.IdSeason == season);
        }

        public async Task<bool> BeValidWeeks(CreateMatchesCommand command, uint season, CancellationToken cancellationToken)
        {
            foreach (var week in command.WeeklyGames)
            {
                if (await _context.Weeks.CountAsync(p => p.IdWeek == week.WeekId && p.FkIdSeason == season) <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> TeamsExistWithinSeason(CreateMatchesCommand command, uint season, CancellationToken cancellationToken)
        {
            foreach (var week in command.WeeklyGames)
            {
                foreach (var game in week.GameList)
                {
                    var blu = await _context.Teams.CountAsync(p => p.IdTeam == game.BlueTeam && p.FkIdSeason == season);
                    if (blu <= 0)
                    {
                        return false;
                    }

                    var red = await _context.Teams.CountAsync(p => p.IdTeam == game.RedTeam && p.FkIdSeason == season);
                    if (red <= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> WeeksDontOutlastRegularSeasonWeeks(CreateMatchesCommand command, uint season, CancellationToken cancellationToken)
        {
            var regSeasonWeeks = await _context.Weeks.CountAsync(p => p.WeekType == "n" && p.FkIdSeason == season);

            if (regSeasonWeeks == command.WeeklyGames.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HaveSameAmountGamesPerWeek(List<WeeklyRequest> weeklyGames)
        {
            var length = weeklyGames[0].GameList.Count;
            foreach (var request in weeklyGames)
            {
                if (length != request.GameList.Count)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> HaveAtLeastOneGamePerTeam(CreateMatchesCommand command, uint season, CancellationToken cancellationToken)
        {
            var totalTeams = await _context.Teams.CountAsync(s => s.FkIdSeason == season, cancellationToken);

            foreach (var weeks in command.WeeklyGames)
            {
                var gameAmount = weeks.GameList.Count;

                if ((totalTeams / 2) > gameAmount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
