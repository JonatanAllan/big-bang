using CaliberFS.Template.Bootstrapper.DependencyInjection;
using CaliberFS.Template.IoC.DependencyInjection;

namespace CaliberFS.Template.WebApi.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServerHealthCheck()
                .AddRabbitMQHealthCheck();
            return services;
        }
    }
}
