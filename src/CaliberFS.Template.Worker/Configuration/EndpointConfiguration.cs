using CaliberFS.Template.Worker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaliberFS.Template.Worker.Configuration
{
    public static class EndpointConfiguration
    {
        public static IApplicationBuilder UseMapCustomEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/background", (
                    [FromServices] PeriodicHostedService service) => new PeriodicHostedServiceState(service.IsEnabled, service.LastExecution));

                endpoints.MapMethods("/background", new[] { "PATCH" }, (
                    [FromBody] PeriodicHostedServiceState state,
                    [FromServices]PeriodicHostedService service) =>
                {
                    service.IsEnabled = state.IsEnabled;
                });
            });
            return app;
        }
    }
}
