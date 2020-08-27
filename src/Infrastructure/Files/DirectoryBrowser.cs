using WorldDoomLeague.Application.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WorldDoomLeague.Infrastructure.Files
{
    public class DirectoryBrowser : IDirectoryBrowser
    {
        public IEnumerable<string> GetDirectoryListing(string directoryPath, string searchPattern)
        {
            return Directory.GetFiles(directoryPath).Select(s => Path.GetFileName(s)).ToList();
        }
    }
}