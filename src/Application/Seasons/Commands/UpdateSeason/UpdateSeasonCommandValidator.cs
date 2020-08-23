using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Seasons.Commands.UpdateSeason
{
    public class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSeasonCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.SeasonName)
                .NotEmpty().WithMessage("SeasonName is required.")
                .MaximumLength(64).WithMessage("SeasonName must not exceed 64 characters.")
                .MustAsync(BeUniqueSeasonName).WithMessage("The specified player name already exists.");

            RuleFor(v => v.DateStart)
                .NotEmpty().WithMessage("DateStart is required.");

            RuleFor(v => v.WadId)
                .NotEmpty().WithMessage("WadId is required.")
                .MustAsync(WadFileExists).WithMessage("The specified wad id does not exist.");
        }

        public async Task<bool> BeUniqueSeasonName(UpdateSeasonCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Season
                .Where(w => w.IdSeason != model.SeasonId)
                .AllAsync(p => p.SeasonName != name);
        }

        public async Task<bool> WadFileExists(UpdateSeasonCommand model, uint id, CancellationToken cancellationToken)
        {
            return await _context.Files
                .Where(w => w.IdFile == model.WadId)
                !.AnyAsync();
        }
    }
}
