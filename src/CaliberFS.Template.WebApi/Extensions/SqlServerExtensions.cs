using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Data.Context;
using CaliberFS.Template.Data.Repositories;
using CaliberFS.Template.Data.UnitOfWork;
using CaliberFS.Template.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaliberFS.Template.WebApi.Extensions
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
