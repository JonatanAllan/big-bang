using Enterprise.Template.Application.Services.UnitOfWork;
using Enterprise.Template.Data.Context;
using Enterprise.Template.Data.Repositories;
using Enterprise.Template.Data.UnitOfWork;
using Enterprise.Template.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Template.IoC.DependencyInjection
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
