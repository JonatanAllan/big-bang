using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Data.Context;
using CaliberFS.Template.Data.Repositories;
using CaliberFS.Template.Data.UnitOfWork;
using CaliberFS.Template.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaliberFS.Template.IoC.DependencyInjection
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddCustomSqlServer(
            this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AppDbContext>(
            //    options => options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("sample"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBoardRepository, BoardRepository>();

            return services;
        }

        public static IHealthChecksBuilder AddSqlServerHealthCheck(
            this IHealthChecksBuilder healthChecksBuilder)
        {
            healthChecksBuilder
                .AddDbContextCheck<AppDbContext>("Sql Server", customTestQuery: async (db, cancel) => await db.Boards.AnyAsync(cancellationToken: cancel));

            return healthChecksBuilder;
        }
    }
}
