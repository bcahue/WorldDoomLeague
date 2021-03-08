using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.TradePlayerToFreeAgency
{
    public partial class TradePlayerToFreeAgencyCommand : IRequest<bool>
    {
        public uint Season { get; set; }
        public uint Week { get; set; }
        public uint TradedPlayer { get; set; }
        public uint TradedPlayerFor { get; set; }
        public uint TeamTradedFrom { get; set; }
    }

    public class TradePlayerToFreeAgencyCommandHandler : IRequestHandler<TradePlayerToFreeAgencyCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public TradePlayerToFreeAgencyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(TradePlayerToFreeAgencyCommand request, CancellationToken cancellationToken)
        {
            var tradedFromTeam = await _context.Teams.Where(w => w.IdTeam == request.TeamTradedFrom).FirstOrDefaultAsync();

            byte tradedPlayerCaptain = 0;

            // Trading captains is supported, but not recommmended.

            if (tradedFromTeam.FkIdPlayerCaptain == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerCaptain = request.TradedPlayerFor;
                tradedPlayerCaptain = 1;
            }
            else if (tradedFromTeam.FkIdPlayerFirstpick == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerFirstpick = request.TradedPlayerFor;
            }
            else if (tradedFromTeam.FkIdPlayerSecondpick == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerSecondpick = request.TradedPlayerFor;
            }
            else if (tradedFromTeam.FkIdPlayerThirdpick == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerThirdpick = request.TradedPlayerFor;
            }

            var tradedEntity = new PlayerTransactions
            {
                FkIdPlayer = request.TradedPlayer,
                FkIdPlayerTradedFor = request.TradedPlayerFor,
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamTradedFrom = request.TeamTradedFrom,
                FkIdTeamTradedTo = null,
                PlayerPromotedCaptain = tradedPlayerCaptain
            };

            _context.PlayerTransactions.Add(tradedEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
