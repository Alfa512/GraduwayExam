using System.Collections.Generic;
using System.Threading.Tasks;
using GraduwayExam.Common.Models.ViewModel;

namespace GraduwayExam.Common.Contracts.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskVm> GetAll();
        TaskVm GetById(string id);
        Task<TaskVm> CreateAsync(TaskVm task, string password);
        void UpdateAsync(TaskVm task, string password = null);
        void Delete(string id);
    }
}