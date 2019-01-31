using System.Collections.Generic;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;

namespace GraduwayExam.Common.Contracts.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskVm> GetAll();
        TaskVm GetById(string id);
        IEnumerable<TaskVm> GetByUserId(string userId);
        TaskVm Create(TaskVm task, string userName = null);
        TaskVm Update(TaskVm task);
        void Delete(string id);
        IEnumerable<TaskVm> DomainToViewList(List<GraduwayExam.Data.Models.Task> tasks);
        List<GraduwayExam.Data.Models.Task> ViewToDomainList(List<TaskVm> tasks);
        TaskVm DomainToView(GraduwayExam.Data.Models.Task task);
        GraduwayExam.Data.Models.Task ViewToDomain(TaskVm task);
        IEnumerable<TaskVm> OrderTasks(List<TaskVm> tasks, OrderByTaskFilter filter);
    }
}