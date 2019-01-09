using System;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GraduwayExam.Common.Modules
{
    public static class ServiceModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
