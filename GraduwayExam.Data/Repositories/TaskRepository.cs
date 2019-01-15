using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Data.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationContext context) : base(context)
        {
        }
    }
}