using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExamApp.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/token")]
    [EnableCors("AllowSpecificOrigin")]
    public class TokenController : Controller
    {
        private IUserService _userService;
        private TokenManager _tokenManager;

        public TokenController(IUserService userService)
        {
            _userService = userService;
            _tokenManager = new TokenManager(_userService);
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task Token([FromBody]UserVm userData)
        {
            var encodedJwt = _tokenManager.GenerateToken(userData.Username, userData.Password);

            var response = new
            {
                access_token = encodedJwt,
                username = userData.Username
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}