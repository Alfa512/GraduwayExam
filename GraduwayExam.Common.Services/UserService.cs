using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduwayExam.Common.Contracts.Data;
using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Common.Services
{
    public class UserService : IUserService
    {
        private IDataContext _context;
        //private IUserRepository _repository;

        public UserService(IDataContext context/*, IUserRepository repository*/)
        {
            _context = context;
            //_repository = repository;
            //_userManager = userManager;
            //_userManager = new UserManager<User>();
        }

        public async Task<UserVm> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _context.Users.GetAll().SingleOrDefault(x => x.UserName == username);

            if (user == null)
                return null;

            //var res = await _repository.CheckPasswordAsync(user, password);

            //if (!res)
                return null;

            return DomainToViewList(user);
        }

        public IEnumerable<UserVm> GetAll()
        {
            return DomainToViewList(_context.Users.GetAll().ToList());
        }

        public UserVm GetById(string id)
        {
            return DomainToViewList(_context.Users.GetAll().FirstOrDefault(u => u.Id == id));
        }

        public async Task<UserVm> CreateAsync(UserVm userModel, string password)
        {
            var user = Mapper.Map<User>(userModel);
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password is required");

            //var result = await _userManager.CreateAsync(user, userModel.Password);
            //if (result.Succeeded)
            {
                //await _userManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                return userModel;
            }

            return null;
        }

        public async void UpdateAsync(UserVm userParam, string password = null)
        {
            var userModel = Mapper.Map<User>(userParam);

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
                //await _userManager.ChangePasswordAsync(user, userParam.CurrentPassword, password);
                user = _context.Users.GetAll().FirstOrDefault(u => u.Id == userModel.Id);
            }

            //await _userManager.UpdateAsync(user);
            _context.Users.Update(user);
            _context.Commit();
        }

        public void Delete(string id)
        {
            var user = _context.Users.GetAll().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Delete(user);
                _context.Commit();
            }
        }

        public IEnumerable<UserVm> DomainToViewList(List<User> users)
        {
            var vUsers = new List<UserVm>();

            if (users != null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    vUsers.Add(Mapper.Map<UserVm>(user));
                }
            }

            return vUsers;
        }
        public List<User> ViewToDomainList(List<UserVm> users)
        {
            var vUsers = new List<User>();

            if (users != null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    vUsers.Add(Mapper.Map<User>(user));
                }
            }

            return vUsers;
        }

        public UserVm DomainToViewList(User user)
        {
            return Mapper.Map<UserVm>(user);
        }

        public User ViewToDomain(UserVm user)
        {
            return Mapper.Map<User>(user);
        }
    }
}