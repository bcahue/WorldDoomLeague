using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IDirectoryBrowser
    {
        IEnumerable<string> GetDirectoryListing(string directoryPath, string searchPattern);
    }
}
