using GraduwayExam.Common.Contracts.Data;
using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Data.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        private IDataContext _context;
        public TaskRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public Task UpdateTask(Task task)
        {
            using (_context)
            {
                var mTask = _context.Update(task);
                return mTask.Entity;
            }
        }
    }
}