using System;
using AutoMapper;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/auth")]
    [EnableCors("AllowSpecificOrigin")]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private readonly IConfigurationService _configurationService;

        public AccountController(IUserService userService, IConfigurationService configurationService)
        {
            _userService = userService;
            _configurationService = configurationService;
        }

        /*public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }*/

        /**ToDo Not Implemented*/
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] UserVm userModel)
        {
            var user = _userService.AuthenticateAsync(userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var res = Mapper.Map<UserVm>(user);
            return Ok(res);
            /*
            return Ok(new 
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });*/
        }

        /**ToDo Not Implemented*/
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserVm userModel)
        {
            // map dto to entity
            //var user = _mapper.Map<User>(userModel);

            try
            {
                // save 
                _userService.Create(userModel, userModel.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}