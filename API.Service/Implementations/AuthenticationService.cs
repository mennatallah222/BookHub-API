﻿using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthenticationService(JwtSettings jwtSettings,
                                     IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            //get claims
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            };


            var jwtToken = new JwtSecurityToken(
                                _jwtSettings.Issuer,
                                _jwtSettings.Audience,
                                claims,
                                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExipreDate),
                                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                                SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


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
            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var rand = new byte[32];
            var randGenerator = RandomNumberGenerator.Create();
            randGenerator.GetBytes(rand);
            return Convert.ToBase64String(rand);
        }
    }
}
