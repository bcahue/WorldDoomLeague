using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.PlayerTransaction.Commands.CreatePlayerTransaction
{
    public partial class CreatePlayerTransactionCommand : IRequest<uint>
    {
        public uint TradedPlayer { get; set; }
        public uint Season { get; set; }
        public uint Week { get; set; }
        public uint? TeamTradedFrom { get; set; }
        public uint? TeamTradedTo { get; set; }
        public bool PlayerPromotedCaptain { get; set; }
    }

    public class CreatePlayerTransactionCommandHandler : IRequestHandler<CreatePlayerTransactionCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayerTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreatePlayerTransactionCommand request, CancellationToken cancellationToken)
        {
            var tradedFromTeam = await _context.Teams.Where(w => w.IdTeam == request.TeamTradedFrom).FirstOrDefaultAsync();

            if (request.TeamTradedFrom != null && tradedFromTeam.FkIdPlayerCaptain == request.TradedPlayer)
            {
                // Remove the captain if he is traded to another team or traded to free agency.
                // This is needed to calculate captain records.
                tradedFromTeam.FkIdPlayerCaptain = null;
            }

            if (request.PlayerPromotedCaptain)
            {
                var tradedToTeam = await _context.Teams.Where(w => w.IdTeam == request.TeamTradedTo).FirstOrDefaultAsync();

                tradedToTeam.FkIdPlayerCaptain = request.TradedPlayer;
            }

            var entity = new PlayerTransactions
            {
                FkIdPlayer = request.TradedPlayer,
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamTradedFrom = request.TeamTradedFrom,
                FkIdTeamTradedTo = request.TeamTradedTo,
                PlayerPromotedCaptain = request.PlayerPromotedCaptain ? (byte)1 : (byte)0
            };

            _context.PlayerTransactions.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.TransactionId;
        }
    }
}
