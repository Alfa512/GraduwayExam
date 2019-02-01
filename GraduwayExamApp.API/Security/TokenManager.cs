using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace GraduwayExamApp.API.Security
{
    public class TokenManager
    {
        private IUserService _userService;
        private PasswordHasher<UserVm> _passwordHasher;

        public TokenManager(IUserService userService)
        {
            _userService = userService;
            _passwordHasher = new PasswordHasher<UserVm>();
        }

        public string GenerateToken(string username, string password)
        {
            var identity = GetIdentity(username, password);

            return GetJwt(identity);
        }

        public string UpdateToken(string username)
        {
            var identity = GetClaimsIdentity(username);

            return GetJwt(identity);
        }

        public string GetJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _userService.GetForAuth(username);
            if (user == null)
                return null;
            var passwordCheck = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (passwordCheck == PasswordVerificationResult.Success)
            {
                return GetClaimsIdentity(user.Username);
            }

            return null;
        }

        private ClaimsIdentity GetClaimsIdentity(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
