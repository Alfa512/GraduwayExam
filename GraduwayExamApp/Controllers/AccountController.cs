using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduwayExamApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly IConfigurationService _configurationService;

        public AccountController(SignInManager<IdentityUser> signInManager, IUserService userService, IMapper mapper, IConfigurationService configurationService)
        {
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
            _configurationService = configurationService;
        }

        /*public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return result;
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return null;
        }*/

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
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {

            try
            {
                //var res = await _signInManager.SignOutAsync();
                //_logger.LogInformation("User logged out.");
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    //return res;
                    return null;
                }
            }
            catch (ApplicationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}