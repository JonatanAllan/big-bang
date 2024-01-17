﻿using Enterprise.Template.IoC.DependencyInjection;

namespace Enterprise.Template.Consumer.Configuration
{
    public static class HealthCheckExtensions
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
