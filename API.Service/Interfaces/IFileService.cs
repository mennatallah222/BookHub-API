using Microsoft.AspNetCore.Http;

namespace API.Service.Interfaces
{
    public interface IFileService
    {
        public Task<string> UploadImage(string location, IFormFile file);
    }
}
