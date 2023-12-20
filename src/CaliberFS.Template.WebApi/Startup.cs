using CaliberFS.Template.IoC.DependencyInjection;
using CaliberFS.Template.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using CaliberFS.Template.WebApi.Extensions;

namespace CaliberFS.Template.WebApi;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddVersionedApi();
        services.AddSwagger();
        services.AddCustomHealthCheck(configuration);
        services.AddCustomSqlServer(configuration);
        services.AddUseCases();
        services.AddExceptionHandler<CustomExceptionHandler>();
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

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