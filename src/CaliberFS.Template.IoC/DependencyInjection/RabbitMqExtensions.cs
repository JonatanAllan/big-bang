using CaliberFS.Template.Application.Services.RabbitMQ;
using CaliberFS.Template.Core.RabbitMQ.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CaliberFS.Template.IoC.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionFactory>(_ =>
            {
                var connection = configuration.GetConnectionString("RabbitMQ")!;
                return new ConnectionFactory { Uri = new Uri(connection), DispatchConsumersAsync = true};
            });
            return services;
        }

        public static IHealthChecksBuilder AddRabbitMQHealthCheck(
            this IHealthChecksBuilder healthChecksBuilder)
        {
            healthChecksBuilder
                .AddRabbitMQ();

            return healthChecksBuilder;
        }
    }
}
