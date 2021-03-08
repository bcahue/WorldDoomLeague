using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatch
{
    public partial class CreateMatchCommand : IRequest<uint>
    {
        public uint Season { get; set; }
        public uint Week { get; set; }
        public uint RedTeam { get; set; }
        public uint BlueTeam { get; set; }
        public DateTime? GameDateTime { get; set; }
        public uint[] GameMapIds { get; set; }
    }

    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateMatchCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var week = await _context.Weeks.FirstOrDefaultAsync(f => f.IdWeek == request.Week, cancellationToken);

            var entity = new Games
            {
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamRed = request.RedTeam,
                FkIdTeamBlue = request.BlueTeam,
                GameDatetime = request.GameDateTime,
                DoubleForfeit = 0,
                GameType = week.WeekType
            };

            List<GameMaps> maps = new List<GameMaps>();

            foreach (var map in request.GameMapIds)
            {
                maps.Add(new GameMaps {
                    FkIdGameNavigation = entity,
                    FkIdMap = map
                });
            }

            _context.Games.Add(entity);
            _context.GameMaps.AddRange(maps);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdGame;
        }
    }
}
