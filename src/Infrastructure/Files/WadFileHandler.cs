using WorldDoomLeague.Application.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WorldDoomLeague.Infrastructure.Files
{
    public class WadFileHandler : IWadFileHandler
    {
        public async Task<string> SaveWadFile(string directoryPath, string fileName, IFormFile wadFile)
        {
            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = File.Create(filePath))
            {
                await wadFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}