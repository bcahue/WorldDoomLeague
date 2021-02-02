using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Files.Queries.GetWadFiles
{
    public class GetWadFilesQuery : IRequest<WadFilesVm>
    {
    }

    public class GetWadFilesQueryHandler : IRequestHandler<GetWadFilesQuery, WadFilesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWadFilesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WadFilesVm> Handle(GetWadFilesQuery request, CancellationToken cancellationToken)
        {
            return new WadFilesVm
            {
                WadList = await _context.WadFiles
                    .ProjectTo<WadFilesDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
