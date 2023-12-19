using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Data.Repositories;
using CaliberFS.Template.Data.UnitOfWork;
using CaliberFS.Template.Domain.Interfaces.Repositories;

namespace CaliberFS.Template.WebApi.Extensions
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
