using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Application.MatchModel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WorldDoomLeague.Application.ConfigModels;

namespace WorldDoomLeague.Application.Matches.Commands.ProcessMatch
{
    public partial class ProcessMatchCommand : IRequest<uint>
    {
        public uint MatchId { get; set; }
        public bool FlipTeams { get; set; }
        public IList<RoundObject> GameRounds { get; set; }
    }

    public class ProcessMatchCommandHandler : IRequestHandler<ProcessMatchCommand, uint>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGetMatchJson _getMatchJson;
        private readonly IOptionsSnapshot<DataDirectories> _optionsDelegate;

        public ProcessMatchCommandHandler(IApplicationDbContext context, IGetMatchJson getMatchJson, IOptionsSnapshot<DataDirectories> optionsDelegate)
        {
            _context = context;
            _getMatchJson = getMatchJson;
            _optionsDelegate = optionsDelegate;
        }

        public async Task<uint> Handle(ProcessMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Games.Where(w => w.IdGame == request.MatchId).FirstOrDefaultAsync(cancellationToken);

            // Get every round into their objects via json.

            List<Round> rounds = new List<Round>();
            
            foreach (var round in request.GameRounds)
            {
                rounds.Add(await _getMatchJson.GetRoundObject(_optionsDelegate.Value.JsonMatchDirectory, round.RoundFileName));
            }

            // Loop thru each round, replace player names with player ids, and determine winners.

            // Finalize, create all necessary stats objects, and determine who won/lost the game and rounds, and create player records for it accordingly.

            await _context.SaveChangesAsync(cancellationToken);

            return match.IdGame;
        }
    }
}
