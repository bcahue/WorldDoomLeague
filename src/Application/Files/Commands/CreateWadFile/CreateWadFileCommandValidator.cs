using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Files.Commands.CreateWadFile
{
    public class CreateWadFileCommandValidator : AbstractValidator<CreateWadFileCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateWadFileCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.File)
                .NotNull().WithMessage("File is required.")
                .NotEmpty().WithMessage("File is required.");

            RuleFor(v => v.File.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(64).WithMessage("FileName must not exceed 64 characters.")
                .Must(BeZipped).WithMessage("File must be zipped prior to uploading.")
                .MustAsync(BeUniqueFileName).WithMessage("The specified file name already exists.");
        }

        public async Task<bool> BeUniqueFileName(string name, CancellationToken cancellationToken)
        {
            return await _context.WadFiles
                .AllAsync(p => p.FileName != name);
        }

        public bool BeZipped(string name)
        {
            if (System.IO.Path.GetExtension(name) != ".zip")
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
