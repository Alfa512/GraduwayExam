using System;
using System.Configuration;
using System.IO;
using GraduwayExam.Common.Contracts.Services;

namespace GraduwayExam.Common.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string ConnectionString { get { return Path.Combine(_basePath, ConfigurationManager.AppSettings["RestorePasswordMailTemplate"]); } }

        private readonly string _basePath = AppDomain.CurrentDomain.BaseDirectory;

    }
}
