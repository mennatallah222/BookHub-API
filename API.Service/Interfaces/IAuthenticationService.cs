using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace API.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);

        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken token, string refreshToken, DateTime? expireTime);
        public Task<string> ValidateToken(string accessToken);
        public Task<string> ConfirmEmail(int? iserId, string? code);
        public Task<string> SendResestPasswordCode(string email);
    }
}
