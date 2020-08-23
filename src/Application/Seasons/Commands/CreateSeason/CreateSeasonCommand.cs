using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace WorldDoomLeague.Application.Seasons.Commands.CreateSeason
{
    public partial class CreateSeasonCommand : IRequest<uint>
    {
        public uint WadId { get; set; }
        public string SeasonName { get; set; }
        public DateTime DateStart { get; set; }
    }

    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateSeasonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = new Season
            {
                SeasonName = request.SeasonName,
                FkIdWadFile = request.WadId,
                DateStart = request.DateStart
            };

            _context.Season.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdSeason;
        }
    }
}
