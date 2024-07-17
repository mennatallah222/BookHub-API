using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Service.Interfaces
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(User user, string password);

    }
}
