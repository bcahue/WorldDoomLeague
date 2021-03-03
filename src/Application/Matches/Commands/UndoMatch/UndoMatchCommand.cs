using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.Matches.Commands.UndoMatch
{
    public partial class UndoMatchCommand : IRequest<bool>
    {
        public uint Match { get; set; }
    }

    public class UndoMatchCommandHandler : IRequestHandler<UndoMatchCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UndoMatchCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UndoMatchCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var match = await _context.Games.Where(w => w.IdGame == request.Match).FirstOrDefaultAsync(cancellationToken);

                var gamePlayers             = await _context.GamePlayers                .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var roundPlayers            = await _context.RoundPlayers               .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var gameTeamStats           = await _context.GameTeamStats              .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var playerGameOpponents     = await _context.PlayerGameOpponents        .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var playerGameTeammates     = await _context.PlayerGameTeammates        .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var playerGameRecords       = await _context.PlayerGameRecords          .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var playerRoundOpponents    = await _context.PlayerRoundOpponents       .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var playerRoundTeammates    = await _context.PlayerRoundTeammates       .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var playerRoundRecords      = await _context.PlayerRoundRecords         .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var rounds                  = await _context.Rounds                     .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsAccuracy           = await _context.StatsAccuracyData          .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsAccuracyWithFlag   = await _context.StatsAccuracyWithFlagData  .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsDamage             = await _context.StatsDamageData            .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsDamageCarrier      = await _context.StatsDamageWithFlagData    .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsKills              = await _context.StatsKillData              .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsCarrierKills       = await _context.StatsKillCarrierData       .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsPickupData         = await _context.StatsPickupData            .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);
                var statsRounds             = await _context.StatsRounds                .Where(w => w.FkIdGame == request.Match).ToListAsync(cancellationToken);

                _context.GamePlayers.RemoveRange(gamePlayers);
                _context.RoundPlayers.RemoveRange(roundPlayers);
                _context.GameTeamStats.RemoveRange(gameTeamStats);
                _context.PlayerGameOpponents.RemoveRange(playerGameOpponents);
                _context.PlayerGameTeammates.RemoveRange(playerGameTeammates);
                _context.PlayerGameRecords.RemoveRange(playerGameRecords);
                _context.PlayerRoundOpponents.RemoveRange(playerRoundOpponents);
                _context.PlayerRoundTeammates.RemoveRange(playerRoundTeammates);
                _context.PlayerRoundRecords.RemoveRange(playerRoundRecords);
                _context.Rounds.RemoveRange(rounds);
                _context.StatsAccuracyData.RemoveRange(statsAccuracy);
                _context.StatsAccuracyWithFlagData.RemoveRange(statsAccuracyWithFlag);
                _context.StatsDamageData.RemoveRange(statsDamage);
                _context.StatsDamageWithFlagData.RemoveRange(statsDamageCarrier);
                _context.StatsKillData.RemoveRange(statsKills);
                _context.StatsKillCarrierData.RemoveRange(statsCarrierKills);
                _context.StatsPickupData.RemoveRange(statsPickupData);
                _context.StatsRounds.RemoveRange(statsRounds);

                match.FkIdTeamWinner    = null;
                match.TeamWinnerColor   = null;
                match.FkIdTeamForfeit   = null;
                match.TeamForfeitColor  = null;
                match.DoubleForfeit     = 0;

                await _context.SaveChangesAsync(cancellationToken);

                _context.Database.CommitTransaction();
            }

            return true;
        }
    }
}
