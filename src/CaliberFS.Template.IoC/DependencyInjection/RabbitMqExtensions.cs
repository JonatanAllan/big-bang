using CaliberFS.Template.Core.RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CaliberFS.Template.Bootstrapper.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>()!;
            services.AddSingleton<IConnectionFactory>(_ =>
            {
                var connection = $"amqp://{options.UserName}:{options.Password}@{options.HostName}:{options.Port}";
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
