using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WorldDoomLeague.Application.Engine.Commands.UpdateEngine
{
    public class UpdateEngineCommandValidator : AbstractValidator<UpdateEngineCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEngineCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.EngineId)
                .NotEmpty().WithMessage("EngineId is required.");

            RuleFor(v => v.EngineName)
                .NotEmpty().WithMessage("EngineName is required.")
                .MaximumLength(64).WithMessage("EngineName must not exceed 64 characters.")
                .MustAsync(BeUniqueEngineName).WithMessage("The specified engine name already exists.");

            RuleFor(v => v.EngineUrl)
                .NotEmpty().WithMessage("EngineUrl is required.");
        }

        public async Task<bool> BeUniqueEngineName(UpdateEngineCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Engines
                .Where(w => w.IdEngine != model.EngineId)
                .AllAsync(p => p.EngineName != name);
        }
    }
}
