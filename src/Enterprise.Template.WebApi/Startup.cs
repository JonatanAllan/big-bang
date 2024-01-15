using Enterprise.Logging.SDK.Configuration;
using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.WebApi.Configuration;
using Enterprise.Template.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Enterprise.Template.WebApi;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddVersionedApi();
        services.AddSwagger();
        services.AddCustomHealthCheck(configuration);
        services.AddCustomSqlServer(configuration);
        services.AddApplications();
        services.AddExceptionHandler<CustomExceptionHandler>();

        // RabbitMQ
        services.AddRabbitMq(configuration);
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.ConfigureLoggingMiddleware();

        app.UseVersionedSwagger(provider);
        app.UseExceptionHandler(_ => { });
        app.UseRouting();

        app.UseCors(ops => ops.AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader());

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseHealthChecks();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}