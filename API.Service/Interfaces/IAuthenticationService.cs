using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;

namespace API.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtAuthResult> GetRefreshToken(string AccessTpken, string RefreshToken);
        public Task<string> ValidateToken(string accessToken);
    }
}
