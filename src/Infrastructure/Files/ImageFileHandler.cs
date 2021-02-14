using WorldDoomLeague.Application.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WorldDoomLeague.Infrastructure.Files
{
    public class ImageFileHandler : IImageFileHandler
    {
        public async Task<string> SaveImageFile(string fileName, IFormFile imageFile)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            using (var stream = File.Create(filePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}