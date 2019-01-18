using System.Collections.Generic;
using System.Threading.Tasks;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Common.Contracts.Services
{
    public interface IUserService
    {
        Task<UserVm> AuthenticateAsync(string username, string password);
        IEnumerable<UserVm> GetAll();
        UserVm GetById(string id);
        Task<UserVm> CreateAsync(UserVm user, string password);
        Task<UserVm> UpdateAsync(UserVm user, string password = null);
        void Delete(string id);
        IEnumerable<UserVm> OrderUsers(List<UserVm> users, OrderByUserFilter filter);
    }
}