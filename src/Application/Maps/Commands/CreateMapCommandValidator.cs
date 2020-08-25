using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Maps.Commands.CreateMap
{
    public class CreateMapCommandValidator : AbstractValidator<CreateMapCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMapCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.MapName)
                .NotEmpty().WithMessage("MapName is required.")
                .MaximumLength(64).WithMessage("MapName must not exceed 64 characters.")
                .MustAsync(BeUniqueMapName).WithMessage("The specified map name already exists.");

            RuleFor(v => v.MapNumber)
                .NotEmpty().WithMessage("MapNumber is required.");

            RuleFor(v => v.MapPack)
                .NotEmpty().WithMessage("MapPack is required.")
                .MaximumLength(64).WithMessage("MapPack must not exceed 64 characters.");
        }

        public async Task<bool> BeUniqueMapName(string name, CancellationToken cancellationToken)
        {
            return await _context.Maps
                .AllAsync(p => p.MapName != name);
        }
    }
}
