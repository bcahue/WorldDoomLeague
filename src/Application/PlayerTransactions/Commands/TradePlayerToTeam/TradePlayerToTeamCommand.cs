using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.TradePlayerToTeam
{
    public partial class TradePlayerToTeamCommand : IRequest<bool>
    {
        public uint Season { get; set; }
        public uint Week { get; set; }
        public uint TradedPlayer { get; set; }
        public uint TradedPlayerFor { get; set; }
        public uint TeamTradedFrom { get; set; }
        public uint TeamTradedTo { get; set; }
    }

    public class TradePlayerToTeamCommandHandler : IRequestHandler<TradePlayerToTeamCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public TradePlayerToTeamCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(TradePlayerToTeamCommand request, CancellationToken cancellationToken)
        {
            var tradedFromTeam = await _context.Teams.Where(w => w.IdTeam == request.TeamTradedFrom).FirstOrDefaultAsync();
            var tradedToTeam = await _context.Teams.Where(w => w.IdTeam == request.TeamTradedTo).FirstOrDefaultAsync();

            byte tradedPlayerCaptain = 0;

            // Trading captains is supported, but not recommmended.

            if (tradedFromTeam.FkIdPlayerCaptain == request.TradedPlayer)
            {
                tradedFromTeam.FkIdPlayerCaptain = request.TradedPlayerFor;
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


            if (tradedToTeam.FkIdPlayerCaptain == request.TradedPlayerFor)
            {
                tradedToTeam.FkIdPlayerCaptain = request.TradedPlayer;
                tradedPlayerCaptain = 1;
            }
            else if (tradedToTeam.FkIdPlayerFirstpick == request.TradedPlayerFor)
            {
                tradedToTeam.FkIdPlayerFirstpick = request.TradedPlayer;
            }
            else if (tradedToTeam.FkIdPlayerSecondpick == request.TradedPlayerFor)
            {
                tradedToTeam.FkIdPlayerSecondpick = request.TradedPlayer;
            }
            else if (tradedToTeam.FkIdPlayerThirdpick == request.TradedPlayerFor)
            {
                tradedToTeam.FkIdPlayerThirdpick = request.TradedPlayer;
            }

            var tradedEntity = new PlayerTransactions
            {
                FkIdPlayer = request.TradedPlayer,
                FkIdPlayerTradedFor = request.TradedPlayerFor,
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamTradedFrom = request.TeamTradedFrom,
                FkIdTeamTradedTo = request.TeamTradedTo,
                PlayerPromotedCaptain = tradedPlayerCaptain
            };

            _context.PlayerTransactions.Add(tradedEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
