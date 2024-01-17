using Enterprise.Template.Data;
using Enterprise.Template.Data.Repositories;
using Enterprise.Template.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Template.IoC.DependencyInjection
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBoardRepository, BoardRepository>();
            return services;
        }

        public static IHealthChecksBuilder AddSqlServerHealthChecks(
            this IHealthChecksBuilder healthChecksBuilder, IConfiguration configuration)
        {
            healthChecksBuilder
                .AddSqlServer(connectionString: GetConnectionByName(ConnectionStrings.TemplateApp, configuration),  name: "Sql Server A");
            return healthChecksBuilder;
        }

        private static string GetConnectionByName(string name, IConfiguration configuration)
        {
           var connections =  configuration.GetSection("AppSettings:ConnectionStrings").Get<List<ConnectionItem>>()!;
           return connections.FirstOrDefault(x => x.Name == name)?.ConnectionString;
        }
        
    }

    record ConnectionItem(string Name, string ConnectionString);
}
