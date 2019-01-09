using System;
using GraduwayExam.Common.Contracts.Repositories;

namespace GraduwayExam.Common.Contracts.Data
{
    public interface IDataContext : IDisposable
    {
        void Commit();
        IUserRepository Users { get; }
    }
}
