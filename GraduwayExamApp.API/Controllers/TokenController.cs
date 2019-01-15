using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/token")]
    [EnableCors("AllowSpecificOrigin")]
    public class TokenController : Controller

    {

        private IConfiguration _config;

        public TokenController(IConfiguration config)

        {

            _config = config;

        }

        [AllowAnonymous]

        [HttpPost]

        public dynamic Post([FromBody]LoginVm login)
        {
            IActionResult response = Unauthorized();

            var user = Authenticate(Mapper.Map<UserVm>(login));

            if (user != null)

            {

                var tokenString = BuildToken(user);

                response = Ok(new { token = tokenString });

            }

            return response;
        }

        private string BuildToken(UserVm user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],

                _config["Jwt:Issuer"],

                expires: DateTime.Now.AddMinutes(30),

                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
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