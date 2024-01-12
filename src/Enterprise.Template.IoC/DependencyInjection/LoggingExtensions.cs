using Enterprise.Configuration.Extensions;
using Enterprise.Logging.Models;
using Enterprise.Logging.SDK.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Enterprise.Template.IoC.DependencyInjection
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddLoggingSDK(this IServiceCollection services, HostBuilderContext hostContext)
        {
            services.AddConfiguration<LoggingSettings>(hostContext, "LoggingSettings");
            services.ConfigureLoggingSDK(hostContext);
            return services;
        }
    }
}
