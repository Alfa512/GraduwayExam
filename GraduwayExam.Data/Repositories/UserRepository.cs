using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
