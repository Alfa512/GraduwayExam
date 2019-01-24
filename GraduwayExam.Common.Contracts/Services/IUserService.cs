using System.Collections.Generic;
using System.Threading.Tasks;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;

namespace GraduwayExam.Common.Contracts.Services
{
    public interface IUserService
    {
        Task<UserVm> AuthenticateAsync(string username, string password);
        IEnumerable<UserVm> GetAll();
        UserVm GetById(string id);
        UserVm GetByUserName(string userName);
        UserVm GetForAuth(string userName);
        UserVm Create(UserVm user, string password);
        UserVm Update(UserVm user, string password = null);
        void Delete(string id);
        IEnumerable<UserVm> OrderUsers(List<UserVm> users, OrderByUserFilter filter);
    }
}