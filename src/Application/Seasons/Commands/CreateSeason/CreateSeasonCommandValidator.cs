using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateSeasonCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.SeasonName)
                .NotEmpty().WithMessage("SeasonName is required.")
                .MaximumLength(64).WithMessage("SeasonName must not exceed 64 characters.")
                .MustAsync(BeUniqueSeasonName).WithMessage("The specified player name already exists.");

            RuleFor(v => v.EnginePlayed)
                .NotEmpty().WithMessage("EnginePlayed is required.")
                .MaximumLength(64).WithMessage("EnginePlayed must not exceed 64 characters.");

            RuleFor(v => v.SeasonDateStart)
                .NotEmpty().WithMessage("DateStart is required.");

            RuleFor(v => v.WadId)
                .NotEmpty().WithMessage("WadId is required.")
                .MustAsync(WadFileExists).WithMessage("The specified wad id does not exist.");
        }

        public async Task<bool> BeUniqueSeasonName(CreateSeasonCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Season
                .AllAsync(p => p.SeasonName != name);
        }

        public async Task<bool> WadFileExists(CreateSeasonCommand model, uint id, CancellationToken cancellationToken)
        {
            return await _context.Files
                .Where(w => w.IdFile == model.WadId)
                !.AnyAsync();
        }
    }
}
