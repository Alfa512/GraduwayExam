using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GraduwayExam.Common.Contracts.Maps;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;


namespace GraduwayExam.Maps
{
    public class UserMap : IUserMap

    {

        IUserService userService;

        public UserMap(IUserService service)

        {

            userService = service;

        }

        public UserVm Create(UserVm viewModel)

        {

            User user = ViewModelToDomain(viewModel);
            return new UserVm();
            //return DomainToViewModel(userService.CreateAsync(user, viewModel.Password));

        }

        public void Update(UserVm viewModel)

        {

            User user = ViewModelToDomain(viewModel);

            //userService.UpdateAsync(user);

        }

        public void Delete(string id)

        {

            userService.Delete(id);

        }

        public List<UserVm> GetAll()

        {

            return userService.GetAll().ToList();

        }

        public UserVm DomainToViewModel(User domain)

        {

            var model = Mapper.Map<UserVm>(domain);


            return model;

        }

        public List<UserVm> DomainToViewModel(List<User> domain)

        {

            var model = new List<UserVm>();

            foreach (User of in domain)

            {

                model.Add(Mapper.Map<UserVm>(of));

            }

            return model;

        }

        public User ViewModelToDomain(UserVm officeViewModel)

        {
            var domain = Mapper.Map<User>(officeViewModel);

            return domain;

        }

    }
}
