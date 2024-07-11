using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
    }
}
