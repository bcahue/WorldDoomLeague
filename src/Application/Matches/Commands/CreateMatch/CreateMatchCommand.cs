using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatch
{
    public partial class CreateMatchCommand : IRequest<uint>
    {
        public uint Season { get; set; }
        public uint Week { get; set; }
        public uint RedTeam { get; set; }
        public uint BlueTeam { get; set; }
        public DateTime? GameDateTime { get; set; }
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
            var entity = new Games
            {
                FkIdSeason = request.Season,
                FkIdWeek = request.Week,
                FkIdTeamRed = request.RedTeam,
                FkIdTeamBlue = request.BlueTeam,
                GameDatetime = request.GameDateTime,
                DoubleForfeit = 0
            };

            _context.Games.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdGame;
        }
    }
}
