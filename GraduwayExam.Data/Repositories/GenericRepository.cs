using System.Linq;
using GraduwayExam.Common.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraduwayExam.Data.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext Context;

        protected GenericRepository(ApplicationContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public EntityEntry<T> Create(T entity)
        {
            return Context.Set<T>().Add(entity);
        }

        public T Update(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
                entry = Context.Entry(entity);
            }

            entry.State = EntityState.Modified;

            return entity;
        }

        public EntityEntry<T> Delete(T entity)
        {
            return Context.Set<T>().Remove(entity);
        }
    }
}