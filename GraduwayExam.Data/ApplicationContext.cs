using System;
using GraduwayExam.Common.Contracts.Data;
using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Data.Models;
using GraduwayExam.Data.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduwayExam.Data
{
    public class ApplicationContext : IdentityDbContext<User>, IDataContext
    {
        private IConfigurationService _configurationService;
        IUserRepository IDataContext.Users => new UserRepository(this);
        ITaskRepository IDataContext.Tasks => new TaskRepository(this);
        public ApplicationContext(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            Database.EnsureCreated();
            //Database.
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configurationService.ConnectionString);
            //optionsBuilder.


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Conventions.Remove<PluralizingTableNameConvention>();

            #region OneToMany

            builder.Entity<User>()
                //.Has(s => s.User)
                .HasMany(s => s.Tasks)
                .WithOne(s => s.User).
                HasForeignKey(t => t.UserId);

            #endregion
        }

        void IDataContext.Commit()
        {
            SaveChanges();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}
