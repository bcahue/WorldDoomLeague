using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatches
{
    public class NewGameValidator : AbstractValidator<NewGame>
    {
        public NewGameValidator()
        {
            RuleFor(v => v.RedTeam)
                .NotEmpty().WithMessage("RedTeam is required.");

            RuleFor(v => v.BlueTeam)
                .NotEmpty().WithMessage("BlueTeam is required.");
        }
    }
}
