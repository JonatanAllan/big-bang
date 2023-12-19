using CaliberFS.Template.Application;
using CaliberFS.Template.Application.Common.Behaviours;
using FluentValidation;
using MediatR;

namespace CaliberFS.Template.WebApi.Extensions
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
