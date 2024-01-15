using Enterprise.RabbitMQ.Configuration;
using Enterprise.RabbitMQ.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Enterprise.Template.IoC.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>()!;
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings"));
            services.ConfigureRabbitMQ(null);

            // Required for health checks
            services.AddSingleton<IConnectionFactory>(_ =>
            {
                var connection = $"amqp://{options.Username}:{options.Password}@{options.HostName}:{options.Port}";
                return new ConnectionFactory { Uri = new Uri(connection), DispatchConsumersAsync = true };
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
