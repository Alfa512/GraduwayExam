using System.Collections.Generic;
using System.Linq;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/user")]
    [EnableCors("AllowSpecificOrigin")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("list")]
        public List<UserVm> List()
        {
            var users = _userService.GetAll().ToList();
            users.Add(new UserVm()
            {
                Id = "123",
                FirstName = "Max",
                LastName = "Mad",
                Position = "Crazy Driver"
            });
            users.Add(new UserVm()
            {
                Id = "133",
                FirstName = "Jack",
                LastName = "Carver",
                Position = "Developer"
            });
            return users;
        }

        [Route("create")]
        public string Create(UserVm user)
        {
            var str = "name: jack, lastname: smith";
            return str;
        }
    }
}