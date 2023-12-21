using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CaliberFS.Template.IoC.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(_ =>
            {
                var connection = configuration.GetConnectionString("RabbitMQ")!;
                return new ConnectionFactory { Uri = new Uri(connection) };
            });
            return services;
        }
    }
}
