using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.PromotePlayerToCaptain
{
    public partial class PromotePlayerToCaptainCommand : IRequest<bool>
    {
        public uint Season { get; set; }
        public uint Week { get; set; }
        public uint PlayerPromotedCaptain { get; set; }
        public uint Team { get; set; }
    }

    public class PromotePlayerToCaptainCommandHandler : IRequestHandler<PromotePlayerToCaptainCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public PromotePlayerToCaptainCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(PromotePlayerToCaptainCommand request, CancellationToken cancellationToken)
        {
            var tradedFromTeam = await _context.Teams.Where(w => w.IdTeam == request.Team).FirstOrDefaultAsync();

            byte tradedPlayerCaptain = 1;

            uint? oldCaptainId = tradedFromTeam.FkIdPlayerCaptain;

            if (tradedFromTeam.FkIdPlayerFirstpick == request.PlayerPromotedCaptain)
            {
                tradedFromTeam.FkIdPlayerFirstpick = oldCaptainId;

            }
            else if (tradedFromTeam.FkIdPlayerSecondpick == request.PlayerPromotedCaptain)
            {
                tradedFromTeam.FkIdPlayerSecondpick = oldCaptainId;
            }
            else if (tradedFromTeam.FkIdPlayerThirdpick == request.PlayerPromotedCaptain)
            {
                tradedFromTeam.FkIdPlayerThirdpick = oldCaptainId;
            }

            tradedFromTeam.FkIdPlayerCaptain = request.PlayerPromotedCaptain;

            var tradedEntity = new PlayerTransactions
            {
                FkIdPlayer = (uint)oldCaptainId,
                FkIdPlayerTradedFor = request.PlayerPromotedCaptain,
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamTradedFrom = request.PlayerPromotedCaptain,
                FkIdTeamTradedTo = request.PlayerPromotedCaptain,
                PlayerPromotedCaptain = tradedPlayerCaptain
            };

            _context.PlayerTransactions.Add(tradedEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
