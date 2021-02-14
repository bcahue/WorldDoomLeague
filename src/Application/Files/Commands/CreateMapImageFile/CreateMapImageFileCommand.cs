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
using System.Transactions;

namespace WorldDoomLeague.Application.Files.Commands.CreateMapImageFile
{
    public partial class CreateMapImageFileCommand : IRequest<uint>
    {
        public IFormFile File { get; set; }

        public string Caption { get; set; }

        public uint MapId { get; set; }
    }

    public class CreateWadFileCommandHandler : IRequestHandler<CreateMapImageFileCommand, uint>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeOffset _dateTimeOffset;
        private readonly IImageFileHandler _imageFileHandler;

        public CreateWadFileCommandHandler(IApplicationDbContext context, IDateTimeOffset dateTimeOffset, IImageFileHandler imageFileHandler)
        {
            _context = context;
            _dateTimeOffset = dateTimeOffset;
            _imageFileHandler = imageFileHandler;
        }

        public async Task<uint> Handle(CreateMapImageFileCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.Database.BeginTransaction();
            long size = request.File.Length;

            await _imageFileHandler.SaveImageFile(request.File.FileName, request.File);

            var imageFile = new ImageFiles
            {
                FileName = request.File.FileName,
                FileSize = (uint)size,
                Caption = request.Caption,
                UploadDate = _dateTimeOffset.UtcNow.UtcDateTime
            };

            _context.ImageFiles.Add(imageFile);

            await _context.SaveChangesAsync(cancellationToken);

            var mapImage = new MapImages
            {
                FkIdImageFile = imageFile.IdFile,
                FkIdMap = request.MapId
            };

            _context.MapImages.Add(mapImage);

            await _context.SaveChangesAsync(cancellationToken);

            transaction.Commit();

            return imageFile.IdFile;
        }
    }
}
