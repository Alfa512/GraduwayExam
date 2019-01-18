using GraduwayExam.Data.Models;

namespace GraduwayExam.Common.Contracts.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        Task UpdateTask(Task task);
    }
}