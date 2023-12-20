using CaliberFS.Template.IoC.DependencyInjection;

namespace CaliberFS.Template.WebApi.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServerHealthCheck();
            return services;
        }
    }
}
