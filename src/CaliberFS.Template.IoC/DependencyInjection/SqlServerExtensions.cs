using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Data.Repositories;
using CaliberFS.Template.Data.UnitOfWork;
using CaliberFS.Template.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaliberFS.Template.IoC.DependencyInjection
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddCustomSqlServer(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbSession, DbSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBoardRepository, BoardRepository>();

            return services;
        }

        public static IHealthChecksBuilder AddSqlServerHealthCheck(
            this IHealthChecksBuilder healthChecksBuilder)
        {
            return healthChecksBuilder;
        }
    }
}
