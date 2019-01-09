using System;
using AutoMapper;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduwayExamApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly IConfigurationService _configurationService;

        public AccountController(IUserService userService, IMapper mapper, IConfigurationService configurationService)
        {
            _userService = userService;
            _mapper = mapper;
            _configurationService = configurationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserVm userModel)
        {
            var user = _userService.AuthenticateAsync(userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var res = _mapper.Map<UserVm>(user);
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

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserVm userModel)
        {
            // map dto to entity
            //var user = _mapper.Map<User>(userModel);

            try
            {
                // save 
                _userService.CreateAsync(userModel, userModel.Password);
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