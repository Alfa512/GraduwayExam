using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;
using GraduwayExam.Maps;

namespace GraduwayExam.Common.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserVm> OrderUsers(List<UserVm> users, OrderByUserFilter filter)
        {
            switch (filter)
            {
                case OrderByUserFilter.ByNameAsk:
                    return users.OrderBy(u => u.FirstName);
                case OrderByUserFilter.ByNameDesc:
                    return users.OrderByDescending(u => u.FirstName);
                case OrderByUserFilter.ByLastNameAsc:
                    return users.OrderBy(u => u.LastName);
                case OrderByUserFilter.ByLastNameDesc:
                    return users.OrderByDescending(u => u.LastName);
                case OrderByUserFilter.ByPositionAsk:
                    return users.OrderBy(u => u.Position);
                case OrderByUserFilter.ByPositionDesc:
                    return users.OrderByDescending(u => u.Position);

                default:
                {
                    return users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
                }
            }
        }

        /*ToDo Not Implemented*/
        public async Task<UserVm> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _repository.GetAll().SingleOrDefault(x => x.UserName == username);

            if (user == null)
                return null;

            //var res = await _repository.CheckPasswordAsync(user, password);

            //if (!res)
                return null;

            return DomainToView(user);
        }

        public IEnumerable<UserVm> GetAll()
        {
            return DomainToViewList(_repository.GetAll().ToList());
        }

        public UserVm GetById(string id)
        {
            return DomainToView(_repository.GetAll().FirstOrDefault(u => u.Id == id));
        }

        public UserVm GetByUserName(string userName)
        {
            return DomainToView(_repository.GetAll().FirstOrDefault(u => string.Equals(u.UserName, userName, StringComparison.CurrentCultureIgnoreCase)));
        }

        public UserVm GetForAuth(string userName)
        {
            var user = _repository.GetAll().FirstOrDefault(u => string.Equals(u.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
                return null;
            var vUser = DomainToView(user);
            vUser.Password = user.PasswordHash;
            return vUser;
        }

        public UserVm Create(UserVm userModel, string passwordHash)
        {
            var user = UserMapper.Mapper().Map<User>(userModel);
            user.PasswordHash = passwordHash;
            var result = _repository.Create(user);
            _repository.SaveCganges();
            if (!string.IsNullOrEmpty(result.Entity.Id))
            {
                return DomainToView(result.Entity);
            }

            return null;
        }

        /*ToDo Not Implemented*/
        public UserVm Update(UserVm userParam, string password = null)
        {
            var userModel = UserMapper.Mapper().Map<User>(userParam);

            var user = _repository.GetAll().FirstOrDefault(u => u.Id == userModel.Id);

            if (user == null)
                throw new ApplicationException("User not found");

            if (userModel.UserName != user.UserName)
            {
                if (_repository.GetAll().Any(x => x.UserName == userModel.UserName))
                    throw new ApplicationException("UserName " + userModel.UserName + " is already taken");
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                // _repository.Update().ChangePasswordAsync(user, userParam.CurrentPassword, password);
                user = _repository.GetAll().FirstOrDefault(u => u.Id == userModel.Id);
            }

            user = ViewToDomain(userParam);

            _repository.Update(user);
            _repository.SaveCganges();

            return GetById(userParam.Id);
        }

        /*ToDo Wasn't Tested*/
        public void Delete(string id)
        {
            var user = _repository.GetAll().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _repository.Delete(user);
                _repository.SaveCganges();
            }
        }

        private IEnumerable<UserVm> DomainToViewList(List<User> users)
        {
            var vUsers = new List<UserVm>();

            if (users != null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    vUsers.Add(UserMapper.Mapper().Map<UserVm>(user));
                }
            }

            return vUsers;
        }
        private List<User> ViewToDomainList(List<UserVm> users)
        {
            var vUsers = new List<User>();

            if (users != null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    vUsers.Add(UserMapper.Mapper().Map<User>(user));
                }
            }

            return vUsers;
        }

        private UserVm DomainToView(User user)
        {
            return UserMapper.Mapper().Map<UserVm>(user);
        }

        private User ViewToDomain(UserVm user)
        {
            return UserMapper.Mapper().Map<User>(user);
        }
    }
}