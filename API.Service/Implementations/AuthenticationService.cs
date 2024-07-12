using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        public AuthenticationService(JwtSettings jwtSettings,
                                     IRefreshTokenRepository refreshTokenRepository,
                                     UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            //get claims
            //     var jwtToken = GenerateJwtToken(user);

            var (jwtToken, accessToken) = GenerateJwtToken(user);

            //refresh token
            var refreshToken = getRefreshToken(user.UserName);

            var userRefreshtoken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExipreDate),
                IsUsed = true,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.ToString(),
                Token = accessToken,
                UserId = user.Id
            };

            await _refreshTokenRepository.AddAsync(userRefreshtoken);

            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken = accessToken;

            return response;
        }

        private RefreshToken getRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExipreDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var rand = new byte[32];
            var randGenerator = RandomNumberGenerator.Create();
            randGenerator.GetBytes(rand);
            return Convert.ToBase64String(rand);
        }


        private (JwtSecurityToken, string) GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };


            var jwtToken = new JwtSecurityToken(
                                _jwtSettings.Issuer,
                                _jwtSettings.Audience,
                                claims,
                                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExipreDate),
                                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                                SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


            return (jwtToken, accessToken);

        }

        public async Task<JwtAuthResult> GetRefreshToken(string AccessToken, string refreshToken)
        {
            //read token to get the claims
            var token = ReadJwtToken(AccessToken);

            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Algorithm is wrong");

            }

            if (token.ValidTo > DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token is not expired");

            }

            //get the user
            var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTrasking()
                                            .FirstOrDefaultAsync(x => x.Token == AccessToken &&
                                                                 x.RefreshToken == refreshToken &&
                                                                 x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                throw new SecurityTokenException("Refresh token is not found");

            }

            //validations on token, refresh it
            if (userRefreshToken.ExpiryTime < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh token is expired");

            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SecurityTokenException("User is not found");

            }
            var (jwtSecurityToken, newToken) = GenerateJwtToken(user);

            var response = new JwtAuthResult();
            response.AccessToken = newToken;

            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = userRefreshToken.ExpiryTime;
            response.refreshToken = refreshTokenResult;

            //generate refresh token
            return response;
        }

        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuer = _jwtSettings.Issuer,
                ValidIssuers = new[]
                    {
                        _jwtSettings.Issuer
                    },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
            };

            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

            try
            {
                if (validator == null)
                {
                    throw new SecurityTokenException("Invalid token");
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
