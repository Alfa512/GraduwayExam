using System.Collections.Generic;
using System.Linq;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/user")]
    [EnableCors("AllowSpecificOrigin")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private PasswordHasher<UserVm> _passwordHasher;
        public UserController(IUserService userService)
        {
            _userService = userService;
            _passwordHasher = new PasswordHasher<UserVm>();
        }

        [AllowAnonymous]
        [Route("list")]
        public List<UserVm> List(int? orderBy)
        {
            var sort = orderBy != null ? (OrderByUserFilter)orderBy : OrderByUserFilter.ByNameAsk;
            var users = _userService.OrderUsers(_userService.GetAll().ToList(), sort);
            return users.ToList();
        }

        [Route("getbyid")]
        public UserVm GetById(string id)
        {
            var user = _userService.GetById(id);
            return user;
        }

        [AllowAnonymous]
        [Route("create")]
        [HttpPost]
        public UserVm Create([FromBody]UserVm user)
        {
            if (user.Password != user.ConfirmPassword)
                return null;
            var passwordHash = _passwordHasher.HashPassword(user, user.Password);
            var newUser = _userService.Create(user, passwordHash);

            return newUser;
        }

        [Authorize]
        [Route("update")]
        public UserVm Update(UserVm user)
        {
            var mUser = _userService.Update(user);
            return mUser;
        }

        [Route("delete")]
        public void Delete(string id)
        {
            _userService.Delete(id);
        }
    }
}