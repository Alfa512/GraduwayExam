using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduwayExam.Common.Contracts.Data;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace GraduwayExam.Common.Services
{
    public class UserService : IUserService
    {
        private IDataContext _context;
        private IMapper _mapper;
        UserManager<User> _userManager;

        public UserService(IDataContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _context.Users.GetAll().SingleOrDefault(x => x.UserName == username);

            if (user == null)
                return null;

            var res = await _userManager.CheckPasswordAsync(user, password);

            if (!res)
                return null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.GetAll();
        }

        public User GetById(string id)
        {
            return _context.Users.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public async Task<UserVm> CreateAsync(UserVm userModel, string password)
        {
            var user = _mapper.Map<User>(userModel);
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password is required");

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                //await _userManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                return userModel;
            }

            return null;
        }

        public async void UpdateAsync(UserVm userParam, string password = null)
        {
            var userModel = _mapper.Map<User>(userParam);

            var user = _context.Users.GetAll().FirstOrDefault(u => u.Id == userModel.Id);

            if (user == null)
                throw new ApplicationException("User not found");

            if (userModel.UserName != user.UserName)
            {
                if (_context.Users.GetAll().Any(x => x.UserName == userModel.UserName))
                    throw new ApplicationException("UserName " + userModel.UserName + " is already taken");
            }

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.UserName = userModel.UserName;

            if (!string.IsNullOrWhiteSpace(password))
            {
                await _userManager.ChangePasswordAsync(user, userParam.CurrentPassword, password);
                user = _context.Users.GetAll().FirstOrDefault(u => u.Id == userModel.Id);
            }

            await _userManager.UpdateAsync(user);
            _context.Users.Update(user);
            _context.Commit();
        }

        public void Delete(string id)
        {
            var user = GetAll().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Delete(user);
                _context.Commit();
            }
        }
    }
}