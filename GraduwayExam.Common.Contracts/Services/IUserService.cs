using System.Collections.Generic;
using System.Threading.Tasks;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Common.Contracts.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(string id);
        Task<UserVm> CreateAsync(UserVm user, string password);
        void UpdateAsync(UserVm user, string password = null);
        void Delete(string id);
    }
}