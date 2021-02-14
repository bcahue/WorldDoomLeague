using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IImageFileHandler
    {
        Task<string> SaveImageFile(string fileName, IFormFile imageFile);
    }
}
