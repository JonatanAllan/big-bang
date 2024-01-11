using Enterprise.Template.Application.Application;
using Enterprise.Template.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Template.IoC.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<IBoardApplication, BoardApplication>();
            services.AddScoped<ISamplePeriodicApplication, SamplePeriodicApplication>();
            return services;
        }
    }
}
