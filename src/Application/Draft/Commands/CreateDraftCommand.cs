using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using FluentValidation.Results;
using WorldDoomLeague.Application.Common.Exceptions;

namespace WorldDoomLeague.Application.Draft.Commands.CreateDraft
{
    public partial class CreateDraftCommand : IRequest<int>
    {
        public uint Season { get; set; }
        public List<DraftRequest> DraftRequestList { get; set; }
    }

    public class CreateDraftCommandHandler : IRequestHandler<CreateDraftCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDraftCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDraftCommand request, CancellationToken cancellationToken)
        {
            var teamCaptains = await _context.Teams.Where(w => w.FkIdSeason == request.Season).Select(s => s.FkIdPlayerCaptain).ToListAsync();
            var players = await _context.Player.Where(w => !teamCaptains.Contains(w.Id)).Select(s => s.Id).ToListAsync(); // FIXME: Implement a free agency pool for this.
            uint i = 0;

            List<PlayerDraft> entities = new List<PlayerDraft>();

            // Draft players
            foreach (var draftNomination in request.DraftRequestList)
            {
                i++;
                var winningCaptainsTeam = await _context.Teams.Where(w => w.FkIdSeason == request.Season && w.FkIdPlayerCaptain == draftNomination.PlayerSoldTo).FirstOrDefaultAsync();
                uint draftPosition = 0;

                if (winningCaptainsTeam == null)
                {
                    throw new InvalidDraftException($"The player with the winning bid was not a captain on a team during season id: {request.Season}. DraftRequestList Element: {i}");
                }

                if (winningCaptainsTeam.FkIdPlayerCaptain == draftNomination.NominatedPlayer)
                {
                    throw new InvalidDraftException($"A captain cannot draft themselves. DraftRequestList Element: {i}");
                }
                else if (winningCaptainsTeam.FkIdPlayerFirstpick == null)
                {
                    winningCaptainsTeam.FkIdPlayerFirstpick = draftNomination.NominatedPlayer;
                    draftPosition = 1;
                }
                else if (winningCaptainsTeam.FkIdPlayerSecondpick == null)
                {
                    winningCaptainsTeam.FkIdPlayerSecondpick = draftNomination.NominatedPlayer;
                    draftPosition = 2;
                }
                else if (winningCaptainsTeam.FkIdPlayerThirdpick == null)
                {
                    winningCaptainsTeam.FkIdPlayerThirdpick = draftNomination.NominatedPlayer;
                    draftPosition = 3;
                }
                else
                {
                    throw new InvalidDraftException($"Cannot draft into a full team. DraftRequestList Element: {i}");
                }

                if (players.Remove(draftNomination.NominatedPlayer) == false)
                {
                    throw new InvalidDraftException($"Cannot draft a player who was already drafted this season. DraftRequestList Element: {i}");
                }

                var entity = new PlayerDraft
                {
                    FkIdPlayerNominated = draftNomination.NominatedPlayer,
                    FkIdPlayerNominating = draftNomination.NominatingPlayer,
                    FkIdPlayerSoldTo = draftNomination.PlayerSoldTo,
                    FkIdTeamSoldTo = winningCaptainsTeam.IdTeam,
                    FkIdSeason = request.Season,
                    SellPrice = draftNomination.SellPrice,
                    TeamDraftPosition = draftPosition,
                    DraftNominationPosition = i
                };

                entities.Add(entity);
            }

            // Additional post-draft validation
            var seasonTeams = await _context.Teams.Where(w => w.FkIdSeason == request.Season).ToListAsync();

            foreach (var team in seasonTeams)
            {
                if (team.FkIdPlayerFirstpick == null) { throw new InvalidDraftException($"The first pick on team {team.TeamName} ({team.IdTeam}) was null."); }
                if (team.FkIdPlayerSecondpick == null) { throw new InvalidDraftException($"The second pick on team {team.TeamName} ({team.IdTeam}) was null."); }
                if (team.FkIdPlayerThirdpick == null) { throw new InvalidDraftException($"The third pick on team {team.TeamName} ({team.IdTeam}) was null."); }
            }

            if (entities.Count != seasonTeams.Count * 3) { throw new InvalidDraftException($"More players were drafted than there are player slots available in teams this season."); }


            // Since everything was good, save and exit.
            _context.PlayerDraft.AddRange(entities);

            await _context.SaveChangesAsync(cancellationToken);

            return entities.Count;
        }
    }
}
