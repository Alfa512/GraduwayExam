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
        public ApplicationContext(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configurationService.ConnectionString);
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
