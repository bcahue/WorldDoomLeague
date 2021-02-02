using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Application.Common.Security;
using WorldDoomLeague.Domain.Entities;
using Microsoft.AspNetCore.Http; // Only for IFormFile. I figure this is better than copying the interface.
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WorldDoomLeague.Application.ConfigModels;

namespace WorldDoomLeague.Application.Files.Commands.CreateWadFile
{
    [Authorize(Roles = "Administrator")]
    public partial class CreateWadFileCommand : IRequest<uint>
    {
        public IFormFile File { get; set; }

        public CreateWadFileCommand(IFormFile file)
        {
            File = file;
        }
    }

    public class CreateWadFileCommandHandler : IRequestHandler<CreateWadFileCommand, uint>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeOffset _dateTimeOffset;
        private readonly IWadFileHandler _wadFileHandler;
        private readonly IOptionsSnapshot<DataDirectories> _optionsDelegate;

        public CreateWadFileCommandHandler(IApplicationDbContext context, IDateTimeOffset dateTimeOffset, IWadFileHandler wadFileHandler, IOptionsSnapshot<DataDirectories> optionsDelegate)
        {
            _context = context;
            _dateTimeOffset = dateTimeOffset;
            _wadFileHandler = wadFileHandler;
            _optionsDelegate = optionsDelegate;
        }

        public async Task<uint> Handle(CreateWadFileCommand request, CancellationToken cancellationToken)
        {
            long size = request.File.Length;

            if (request.File.Length > 0)
            {
                await _wadFileHandler.SaveWadFile(_optionsDelegate.Value.WadRepository, request.File.FileName, request.File);
            }
            else
            {
                throw new EmptyFileException("Wad file was empty.");
            }

            if (System.IO.Path.GetExtension(request.File.FileName) != ".zip")
            {
                throw new NotZippedException("Wad file was not zipped prior to uploading.");
            }

            var entity = new WadFiles
            {
                FileName = request.File.FileName,
                FileSize = (uint)size,
                UploadDate = _dateTimeOffset.UtcNow.UtcDateTime
            };

            _context.WadFiles.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdFile;
        }
    }
}
