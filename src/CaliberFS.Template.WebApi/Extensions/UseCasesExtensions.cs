using CaliberFS.Template.Application.Common.Behaviours;
using MediatR;
using FluentValidation;
using CaliberFS.Template.Application;

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
