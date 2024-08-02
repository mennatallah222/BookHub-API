using API.Infrastructure.Data;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailService _emailService;
        private readonly ApplicationDBContext _dbContext;
        private readonly IEncryptionProvider _encryptionProvider;

        public AuthenticationService(JwtSettings jwtSettings,
                                     IRefreshTokenRepository refreshTokenRepository,
                                     UserManager<User> userManager,
                                     IEmailService emailService,
                                     ApplicationDBContext dBContext)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
            _dbContext = dBContext;
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");

        }
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            //get claims
            //     var jwtToken = GenerateJwtToken(user);

            var (jwtToken, accessToken) = await GenerateJwtToken(user);

            //refresh token
            var refreshToken = getRefreshToken(user.UserName);

            var userRefreshtoken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExipreDate),
                IsUsed = true,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
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


        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
        {
            var claims = await GetClaims(user);

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

        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName)

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken token, string refreshToken, DateTime? expireTime)
        {

            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);

            var response = new JwtAuthResult();
            response.AccessToken = newToken;

            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expireTime;
            response.refreshToken = refreshTokenResult;

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

            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);


                if (validator == null)
                {
                    return "Invalid token";
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }

            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = _refreshTokenRepository.GetTableNoTrasking()
                                            .FirstOrDefault(x => x.Token == accessToken &&
                                                                 x.RefreshToken == refreshToken &&
                                                                 x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryTime < DateTime.UtcNow)
            {

                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);

            }

            return (userId, userRefreshToken.ExpiryTime);
        }

        public async Task<string> ConfirmEmail(int? userId, string? code)
        {
            if (userId == null || code == null) return "ErrorConfirmingEmail";
            var uer = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(uer, code);
            if (!confirmEmail.Succeeded) return "ErrorConfirmingEmail";
            return "Success";
        }

        public async Task<string> SendResestPasswordCode(string email)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return "UserNotFound";

                Random random = new Random();
                string randNum = random.Next(0, 1000000).ToString("06");

                user.Code = randNum;
                var updatedResult = await _userManager.UpdateAsync(user);
                if (!updatedResult.Succeeded) return "ErrorUpdatingTheUser";

                var result = await _emailService.SendEmail(email, "Code to reset your message: " + user.Code, "Reset Password");

                await transaction.CommitAsync();
                return result;
            }

            catch (Exception e)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(e.Message);
                return "Failed";
            }
        }

        public async Task<string> ResestPasswordCode(string code, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return "NotFound";
            var userCode = user.Code;
            if (userCode == code) return "Success";
            return "Failed";
        }

        public async Task<string> ResestPassword(string password, string email)
        {
            var transact = _dbContext.Database.BeginTransaction();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return "NotFound";

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception e)
            {
                await transact.RollbackAsync();
                return "Failure";
            }
        }
    }
}