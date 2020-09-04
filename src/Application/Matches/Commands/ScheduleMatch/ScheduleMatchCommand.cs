using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WorldDoomLeague.Application.Matches.Commands.ScheduleMatch
{
    public partial class ScheduleMatchCommand : IRequest<bool>
    {
        public uint Match { get; set; }
        public DateTime GameDateTime { get; set; }
    }

    public class ScheduleMatchCommandHandler : IRequestHandler<ScheduleMatchCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public ScheduleMatchCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ScheduleMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Games.Where(w => w.IdGame == request.Match).FirstOrDefaultAsync(cancellationToken);

            match.GameDatetime = request.GameDateTime;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
