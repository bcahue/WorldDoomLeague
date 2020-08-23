using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace WorldDoomLeague.Application.Files.Commands.CreateFile
{
    public partial class CreateFileCommand : IRequest<uint>
    {
        public string FileName { get; set; }
        public uint FileSize { get; set; }
    }

    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, uint>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;

        public CreateFileCommandHandler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<uint> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var entity = new GameFiles
            {
                FileName = request.FileName,
                FileSize = request.FileSize,
                UploadDate = _dateTime.Now
            };

            _context.Files.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdFile;
        }
    }
}
