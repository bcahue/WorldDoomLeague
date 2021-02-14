using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WorldDoomLeague.Application.Common.Extensions;
using System.Linq;

namespace WorldDoomLeague.Application.Files.Commands.CreateMapImageFile
{
    public class CreateMapImageFileCommandValidator : AbstractValidator<CreateMapImageFileCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMapImageFileCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.File)
                .NotNull().WithMessage("File is required.")
                .NotEmpty().WithMessage("File is required.")
                .MustAsync(FormFileExtensions.IsImage).WithMessage("File is not an image file.");

            RuleFor(v => v.File.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(64).WithMessage("FileName must not exceed 64 characters.")
                .MustAsync(BeUniqueFileName).WithMessage("The specified file name already exists.");

            RuleFor(v => v.MapId)
                .NotEmpty().WithMessage("MapId is required.")
                .MustAsync(BeValidMap).WithMessage("The specified map id does not exist.");
        }

        public async Task<bool> BeUniqueFileName(string name, CancellationToken cancellationToken)
        {
            return await _context.ImageFiles
                .AllAsync(p => p.FileName != name);
        }

        public async Task<bool> BeValidMap(uint mapId, CancellationToken cancellationToken)
        {
            return await _context.Maps
                .Where(w => w.IdMap == mapId)
                .AnyAsync();
        }
    }
}
