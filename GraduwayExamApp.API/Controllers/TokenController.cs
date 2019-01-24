using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/token")]
    [EnableCors("AllowSpecificOrigin")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private IUserService _userService;
        private PasswordHasher<UserVm> _passwordHasher;

        public TokenController(IConfiguration config, IUserService userService)
        {
            _userService = userService;
            _config = config;
            _passwordHasher = new PasswordHasher<UserVm>();
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task Token([FromBody]UserVm userData)
        {
            var username = userData.Username;

            var identity = GetIdentity(username, userData.Password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _userService.GetForAuth(username);
            if (user == null)
                return null;
            var passwordCheck = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (passwordCheck == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                };
                var claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private UserVm Authenticate(UserVm login)
        {
            UserVm user = null;

            if (login.Username == "pablo" && login.Password == "secret")

            {

                user = new UserVm { LastName = "Pablo" };

            }

            return user;
        }

    }
}