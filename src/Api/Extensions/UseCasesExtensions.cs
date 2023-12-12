using Application;
using Application.Services;

namespace Api.Extensions
{
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped(typeof(Notification<>));

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(IAssemblyMaker).Assembly);
            });

            return services;
        }
    }
}
