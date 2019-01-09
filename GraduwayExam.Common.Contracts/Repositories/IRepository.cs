using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraduwayExam.Common.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        EntityEntry<T> Create(T entity);
        T Update(T entity);
        EntityEntry<T> Delete(T entity);
    }
}