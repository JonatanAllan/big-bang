using CaliberFS.Template.IoC.DependencyInjection;

namespace CaliberFS.Template.Worker.Configuration
{
    public static class HealthCheckExtensions
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
