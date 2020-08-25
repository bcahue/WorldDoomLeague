using WorldDoomLeague.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace WorldDoomLeague.Application.Draft.Commands.CreateDraft
{
    public class CreateDraftCommandValidator : AbstractValidator<CreateDraftCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateDraftCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MustAsync(BeUniqueSeason).WithMessage("The specified season's draft already exists.");

            RuleForEach(v => v.DraftRequestList).ChildRules(draft =>
            {
                draft.RuleFor(x => x.NominatedPlayer)
                    .NotEmpty().WithMessage("NominatedPlayer is required.")
                    .MustAsync(BeExistingPlayer).WithMessage("The specified player does not exist.");

                draft.RuleFor(x => x.NominatingPlayer)
                    .NotEmpty().WithMessage("NominatingPlayer is required.")
                    .MustAsync(BeExistingPlayer).WithMessage("The specified player does not exist.");

                draft.RuleFor(x => x.PlayerSoldTo)
                    .NotEmpty().WithMessage("PlayerSoldTo is required.")
                    .MustAsync(BeExistingPlayer).WithMessage("The specified player does not exist.");

                draft.RuleFor(x => x.SellPrice)
                    .NotEmpty().WithMessage("SellPrice is required.")
                    .Must(SellPriceWithinBounds).WithMessage("SellPrice needs to be higher than 1.");
            });
        }

        public async Task<bool> BeUniqueSeason(uint season, CancellationToken cancellationToken)
        {
            return await _context.PlayerDraft
                .AllAsync(p => p.FkIdSeason != season);
        }

        public async Task<bool> BeExistingPlayer(uint player, CancellationToken cancellationToken)
        {
            return await _context.Player
                .Where(w => w.Id == player)
                !.AnyAsync();
        }

        public bool SellPriceWithinBounds(uint sellprice)
        {
            return sellprice >= 1;
        }
    }
}
