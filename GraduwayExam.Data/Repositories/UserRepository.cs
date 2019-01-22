using GraduwayExam.Common.Contracts.Data;
using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private IDataContext _context;
        public UserRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
