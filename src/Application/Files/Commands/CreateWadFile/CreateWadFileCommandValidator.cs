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

            RuleFor(v => v.File.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(64).WithMessage("FileName must not exceed 64 characters.")
                .MustAsync(BeUniqueFileName).WithMessage("The specified file name already exists.");

            RuleFor(v => v.File)
                .NotEmpty().WithMessage("File is required.");
        }

        public async Task<bool> BeUniqueFileName(string name, CancellationToken cancellationToken)
        {
            return await _context.Files
                .AllAsync(p => p.FileName != name);
        }
    }
}
