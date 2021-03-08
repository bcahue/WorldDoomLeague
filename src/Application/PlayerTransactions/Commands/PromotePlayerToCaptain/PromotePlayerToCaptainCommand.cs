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
        public uint TradedPlayer { get; set; }
        public uint TeamTradedFrom { get; set; }
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
            var tradedFromTeam = await _context.Teams.Where(w => w.IdTeam == request.TeamTradedFrom).FirstOrDefaultAsync();

            byte tradedPlayerCaptain = 1;

            uint? oldCaptainId = tradedFromTeam.FkIdPlayerCaptain;

            if (tradedFromTeam.FkIdPlayerFirstpick == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerFirstpick = oldCaptainId;

            }
            else if (tradedFromTeam.FkIdPlayerSecondpick == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerSecondpick = oldCaptainId;
            }
            else if (tradedFromTeam.FkIdPlayerThirdpick == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerThirdpick = oldCaptainId;
            }

            tradedFromTeam.FkIdPlayerCaptain = request.TradedPlayer;

            var tradedEntity = new PlayerTransactions
            {
                FkIdPlayer = request.TradedPlayer,
                FkIdPlayerTradedFor = request.TradedPlayer,
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamTradedFrom = request.TeamTradedFrom,
                FkIdTeamTradedTo = request.TeamTradedFrom,
                PlayerPromotedCaptain = tradedPlayerCaptain
            };

            _context.PlayerTransactions.Add(tradedEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
