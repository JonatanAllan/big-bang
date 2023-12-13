using Application.Services.UnitOfWork;
using Data.Context;
using Data.Repositories;
using Data.UnitOfWork;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddCustomSqlServer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
                //services.AddDbContext<AppDbContext>(
                //    options => options.UseSqlServer(
                //        configuration.GetConnectionString("DefaultConnection")));

                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("sample"));

                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IBoardRepository, BoardRepository>();

            return services;
        }
    }
}
