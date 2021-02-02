using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Engine.Commands.CreateEngine
{
    public class CreateEngineCommandValidator : AbstractValidator<CreateEngineCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateEngineCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.EngineName)
                .NotEmpty().WithMessage("EngineName is required.")
                .MaximumLength(64).WithMessage("EngineName must not exceed 64 characters.")
                .MustAsync(BeUniqueEngineName).WithMessage("The specified engine name already exists.");

            RuleFor(v => v.EngineUrl)
                .NotEmpty().WithMessage("EngineUrl is required.")
                .MaximumLength(64).WithMessage("EngineUrl must not exceed 64 characters.");
        }

        public async Task<bool> BeUniqueEngineName(CreateEngineCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Engines
                .AllAsync(p => p.EngineName != name);
        }
    }
}
