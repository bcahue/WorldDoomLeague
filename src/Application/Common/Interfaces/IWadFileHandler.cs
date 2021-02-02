using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IWadFileHandler
    {
        Task<string> SaveWadFile(string directoryPath, string fileName, IFormFile wadFile);
    }
}
