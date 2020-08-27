using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.SeasonWeeks.Commands.CreateSeasonWeeks
{
    public partial class CreateSeasonWeeksCommand : IRequest<uint>
    {
        public uint SeasonId { get; set; }
        public DateTime WeekOneDateStart { get; set; }
        public uint NumWeeksRegularSeason { get; set; }
        public uint NumWeeksPlayoffs { get; set; }
    }

    public class CreateSeasonWeeksCommandHandler : IRequestHandler<CreateSeasonWeeksCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateSeasonWeeksCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateSeasonWeeksCommand request, CancellationToken cancellationToken)
        {
            DateTime weeksRollingStart = request.WeekOneDateStart;
            uint weekNumber = 1;

            for (var i = 0; i < request.NumWeeksRegularSeason; i++)
            {
                _context.Weeks.Add(new Weeks { FkIdSeason = request.SeasonId, WeekNumber = weekNumber, WeekType = "n", WeekStartDate = weeksRollingStart });
                weeksRollingStart = weeksRollingStart.AddDays(7);
                weekNumber++;
            }

            for (var i = 0; i < request.NumWeeksPlayoffs; i++)
            {
                _context.Weeks.Add(new Weeks { FkIdSeason = request.SeasonId, WeekNumber = weekNumber, WeekType = "p", WeekStartDate = weeksRollingStart });
                weeksRollingStart = weeksRollingStart.AddDays(7);
                weekNumber++;
            }

            _context.Weeks.Add(new Weeks { FkIdSeason = request.SeasonId, WeekNumber = weekNumber, WeekType = "f", WeekStartDate = weeksRollingStart });

            await _context.SaveChangesAsync(cancellationToken);

            return weekNumber;
        }
    }
}
