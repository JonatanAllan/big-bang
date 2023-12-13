using Application;
using Application.Common.Behaviours;
using MediatR;
using FluentValidation;

namespace Api.Extensions
{
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(IAssemblyMaker).Assembly);
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(IAssemblyMaker).Assembly);
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}
