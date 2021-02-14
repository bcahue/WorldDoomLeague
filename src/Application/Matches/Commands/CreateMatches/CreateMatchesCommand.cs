using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatches
{
    public partial class CreateMatchesCommand : IRequest<uint>
    {
        public uint SeasonId { get; set; }

        public List<WeeklyRequest> WeeklyGames { get; set; }
    }

    public class CreateMatchesCommandHandler : IRequestHandler<CreateMatchesCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateMatchesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateMatchesCommand request, CancellationToken cancellationToken)
        {
            List<Games> games = new List<Games>();
            List<GameMaps> gameMaps = new List<GameMaps>();
            List<WeekMaps> weekMaps = new List<WeekMaps>();

            await _context.Database.BeginTransactionAsync(cancellationToken);

            foreach (var weeks in request.WeeklyGames)
            {
                weekMaps.Add(new WeekMaps
                {
                    FkIdMap = weeks.MapId,
                    FkIdWeek = weeks.WeekId
                });

                foreach (var game in weeks.GameList)
                {
                    var newgame = new Games
                    {
                        FkIdSeason = request.SeasonId,
                        FkIdWeek = weeks.WeekId,
                        FkIdTeamRed = game.RedTeam,
                        FkIdTeamBlue = game.BlueTeam,
                        GameDatetime = game.GameDateTime,
                        GameType = "n"
                    };

                    games.Add(newgame);

                    gameMaps.Add(new GameMaps
                    {
                        FkIdMap = weeks.MapId,
                        FkIdGameNavigation = newgame
                    });
                }
            }

            _context.Games.AddRange(games);
            _context.GameMaps.AddRange(gameMaps);
            _context.WeekMaps.AddRange(weekMaps);

            await _context.SaveChangesAsync(cancellationToken);

            await _context.Database.CommitTransactionAsync(cancellationToken);

            return (uint)games.Count;
        }
    }
}
