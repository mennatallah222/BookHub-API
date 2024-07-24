using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Service.AuthService.Interfaces
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetUserRoleAsync();

    }
}
