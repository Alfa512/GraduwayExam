using System.Collections.Generic;
using System.Linq;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.Enums;
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

        [Route("create")]
        public string Create(UserVm user)
        {
            var str = "name: jack, lastname: smith";
            return str;
        }

        [Route("update")]
        public UserVm Update(UserVm user)
        {
            var mUser = _userService.UpdateAsync(user);
            return mUser.Result;
        }

        [Route("delete")]
        public string Delete(UserVm user)
        {
            var str = "name: jack, lastname: smith";
            return str;
        }
    }
}