using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace API.Service.Interfaces
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(User user, string password, IFormFile file);

    }
}
