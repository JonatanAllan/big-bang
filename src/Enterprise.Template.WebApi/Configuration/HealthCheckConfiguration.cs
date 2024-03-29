﻿using Enterprise.Template.IoC.DependencyInjection;

namespace Enterprise.Template.WebApi.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddRabbitMQHealthCheck()
                .AddSqlServerHealthChecks(configuration);
            return services;
        }
    }
}
