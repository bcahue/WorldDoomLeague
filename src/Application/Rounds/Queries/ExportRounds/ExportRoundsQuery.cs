using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.Rounds.Queries.ExportRounds
{
    public class ExportRoundsQuery : IRequest<ExportRoundsVm>
    {
        public RoundsOutputFileType OutputType { get; }
        public ExportRoundsQuery (RoundsOutputFileType outputType)
        {
            OutputType = outputType;
        }
    }

    public class ExportTodosQueryHandler : IRequestHandler<ExportRoundsQuery, ExportRoundsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportTodosQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportRoundsVm> Handle(ExportRoundsQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportRoundsVm();

            var records = await _context.StatsRounds
                    .Include(i => i.FkIdPlayerNavigation)
                    .Include(i => i.FkIdTeamNavigation)
                    .Include(i => i.FkIdRoundNavigation)
                    .ProjectTo<RoundRecord>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            vm.Content = _fileBuilder.BuildRoundRecordsFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "WDLRounds.csv";

            return await Task.FromResult(vm);
        }
    }
}
