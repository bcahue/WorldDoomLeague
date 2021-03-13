using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.ReverseLastTrade
{
    public partial class ReverseLastTradeCommand : IRequest<bool>
    {
        public uint Season { get; set; }
    }

    public class ReverseLastTradeCommandHandler : IRequestHandler<ReverseLastTradeCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public ReverseLastTradeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ReverseLastTradeCommand request, CancellationToken cancellationToken)
        {
            var lastTrade = await _context.PlayerTransactions.Where(w => w.FkIdSeason == request.Season).LastOrDefaultAsync(cancellationToken);

            if (lastTrade.FkIdTeamTradedTo == null) // Reverse a free agency trade
            {
                var teamTradedFrom = await _context.Teams.Where(w => w.FkIdSeason == lastTrade.FkIdTeamTradedFrom).FirstOrDefaultAsync(cancellationToken);

                if (teamTradedFrom.FkIdPlayerCaptain == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerCaptain = lastTrade.FkIdPlayer;
                }
                else if (teamTradedFrom.FkIdPlayerFirstpick == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerFirstpick = lastTrade.FkIdPlayer;
                }
                else if (teamTradedFrom.FkIdPlayerSecondpick == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerSecondpick = lastTrade.FkIdPlayer;
                }
                else if (teamTradedFrom.FkIdPlayerThirdpick == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerThirdpick = lastTrade.FkIdPlayer;
                }
            }
            else if (lastTrade.FkIdTeamTradedTo != lastTrade.FkIdTeamTradedFrom) // Reverse a team trade
            {
                var teamTradedFrom = await _context.Teams.Where(w => w.FkIdSeason == lastTrade.FkIdTeamTradedFrom).FirstOrDefaultAsync(cancellationToken);
                var teamTradedTo = await _context.Teams.Where(w => w.FkIdSeason == lastTrade.FkIdTeamTradedTo).FirstOrDefaultAsync(cancellationToken);

                if (teamTradedFrom.FkIdPlayerCaptain == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerCaptain = lastTrade.FkIdPlayer;
                }
                else if (teamTradedFrom.FkIdPlayerFirstpick == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerFirstpick = lastTrade.FkIdPlayer;
                }
                else if (teamTradedFrom.FkIdPlayerSecondpick == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerSecondpick = lastTrade.FkIdPlayer;
                }
                else if (teamTradedFrom.FkIdPlayerThirdpick == lastTrade.FkIdPlayer)
                {
                    teamTradedFrom.FkIdPlayerThirdpick = lastTrade.FkIdPlayer;
                }

                if (teamTradedTo.FkIdPlayerCaptain == lastTrade.FkIdPlayerTradedFor)
                {
                    teamTradedTo.FkIdPlayerCaptain = lastTrade.FkIdPlayerTradedFor;
                }
                else if (teamTradedTo.FkIdPlayerFirstpick == lastTrade.FkIdPlayerTradedFor)
                {
                    teamTradedTo.FkIdPlayerFirstpick = lastTrade.FkIdPlayerTradedFor;
                }
                else if (teamTradedTo.FkIdPlayerSecondpick == lastTrade.FkIdPlayerTradedFor)
                {
                    teamTradedTo.FkIdPlayerSecondpick = lastTrade.FkIdPlayerTradedFor;
                }
                else if (teamTradedTo.FkIdPlayerThirdpick == lastTrade.FkIdPlayerTradedFor)
                {
                    teamTradedTo.FkIdPlayerThirdpick = lastTrade.FkIdPlayerTradedFor;
                }
            }
            else if (lastTrade.FkIdTeamTradedTo == lastTrade.FkIdTeamTradedFrom) // Reverse a captain promotion
            {
                var team = await _context.Teams.Where(w => w.FkIdSeason == lastTrade.FkIdTeamTradedFrom).FirstOrDefaultAsync(cancellationToken);
                var oldCaptainId = lastTrade.FkIdPlayer;

                if (team.FkIdPlayerFirstpick == oldCaptainId)
                {
                    team.FkIdPlayerFirstpick = lastTrade.FkIdPlayerTradedFor;
                }
                else if (team.FkIdPlayerSecondpick == oldCaptainId)
                {
                    team.FkIdPlayerSecondpick = lastTrade.FkIdPlayerTradedFor;
                }
                else if (team.FkIdPlayerThirdpick == oldCaptainId)
                {
                    team.FkIdPlayerThirdpick = lastTrade.FkIdPlayerTradedFor;
                }

                team.FkIdPlayerCaptain = oldCaptainId;
            }

            _context.PlayerTransactions.Remove(lastTrade);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
