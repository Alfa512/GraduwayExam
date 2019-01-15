using System;
using System.IO;
using GraduwayExam.Common.Contracts.Services;
using Microsoft.Extensions.Configuration;

namespace GraduwayExam.Common.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString
        {
            get { return _configuration.GetValue<string>("ConnectionString"); }
        }

        //private readonly string _basePath = AppDomain.CurrentDomain.BaseDirectory;
    }
}