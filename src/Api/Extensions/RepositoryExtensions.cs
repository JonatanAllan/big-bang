using Application.Services.UnitOfWork;
using Data.Repositories;
using Data.UnitOfWork;
using Domain.Interfaces.Repositories;

namespace Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbSession, DbSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBoardRepository, BoardRepository>();

            return services;
        }
    }
}
