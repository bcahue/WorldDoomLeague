using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMatchCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(BeValidSeason).WithMessage("The specified season does not exist.");

            RuleFor(v => v.Week)
                .NotEmpty().WithMessage("Week is required.")
                .MustAsync(BeValidWeek).WithMessage("The specified week does not exist.")
                .MustAsync(AssureOnlyOneFinalsGame).WithMessage("The specified week already has a finals game.");

            RuleFor(v => v.RedTeam)
                .NotEmpty().WithMessage("RedTeam is required.")
                .MustAsync(BeValidRedTeam).WithMessage("The red team does not exist within this season.")
                .MustAsync(BeSureNoOtherGamesThatWeekForRed).WithMessage("The red team already has a game scheduled within the specified week.");

            RuleFor(v => v.BlueTeam)
                .NotEmpty().WithMessage("BlueTeam is required.")
                .MustAsync(BeValidBlueTeam).WithMessage("The blue team does not exist within this season.")
                .MustAsync(BeSureNoOtherGamesThatWeekForBlue).WithMessage("The blue team already has a game scheduled within the specified week.");

            RuleFor(v => v.GameMapIds)
                .NotEmpty().WithMessage("GameMapIds is required.");

            RuleForEach(v => v.GameMapIds)
                .MustAsync(BeValidMap).WithMessage("One of the specified GameMapIds does not exist.");
        }

        public async Task<bool> BeValidSeason(uint season, CancellationToken cancellationToken)
        {
            return await _context.Season
                .AnyAsync(p => p.IdSeason == season, cancellationToken);
        }

        public async Task<bool> BeValidWeek(CreateMatchCommand command, uint week, CancellationToken cancellationToken)
        {
            return await _context.Weeks
                !.AnyAsync(p => p.IdWeek == week && p.FkIdSeason == command.Season, cancellationToken);
        }
        public async Task<bool> AssureOnlyOneFinalsGame(CreateMatchCommand command, uint week, CancellationToken cancellationToken)
        {
            /*
            var currentWeek = await _context.Weeks.FirstOrDefaultAsync(p => p.IdWeek == week && p.FkIdSeason == command.Season, cancellationToken);

            if (currentWeek.WeekType == "p" || currentWeek.WeekType == "n")
            {
                return true;
            }
            */

            return await _context.Games
                .CountAsync(p => p.FkIdWeek == week && p.FkIdSeason == command.Season && p.GameType == "f", cancellationToken) <= 0;
        }
        public async Task<bool> BeValidBlueTeam(CreateMatchCommand command, uint blueTeam, CancellationToken cancellationToken)
        {
            return await _context.Teams
                !.AnyAsync(p => p.IdTeam == blueTeam && p.FkIdSeason == command.Season, cancellationToken);
        }

        public async Task<bool> BeValidRedTeam(CreateMatchCommand command, uint redTeam, CancellationToken cancellationToken)
        {
            return await _context.Teams
                !.AnyAsync(p => p.IdTeam == redTeam && p.FkIdSeason == command.Season, cancellationToken);
        }

        public async Task<bool> BeSureNoOtherGamesThatWeekForRed(CreateMatchCommand command, uint redTeam, CancellationToken cancellationToken)
        {
            return await _context.Games
                .CountAsync(p => (p.FkIdTeamBlue == redTeam || p.FkIdTeamRed == redTeam) && p.FkIdWeek == command.Week, cancellationToken) <= 0;
        }

        public async Task<bool> BeValidMap(CreateMatchCommand command, uint mapId, CancellationToken cancellationToken)
        {
            return await _context.Maps
                .AnyAsync(p => p.IdMap == mapId, cancellationToken);
        }

        public async Task<bool> BeSureNoOtherGamesThatWeekForBlue(CreateMatchCommand command, uint blueTeam, CancellationToken cancellationToken)
        {
            return await _context.Games
                .CountAsync(p => (p.FkIdTeamBlue == blueTeam || p.FkIdTeamRed == blueTeam) && p.FkIdWeek == command.Week, cancellationToken) <= 0;
        }
    }
}
