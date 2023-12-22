using CaliberFS.Template.IoC.DependencyInjection;
using CaliberFS.Template.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using CaliberFS.Template.Core.RabbitMQ;
using CaliberFS.Template.Core.RabbitMQ.Producer;
using CaliberFS.Template.WebApi.Configuration;
using CaliberFS.Template.Application.Services.RabbitMQ;
using CaliberFS.Template.Bootstrapper.DependencyInjection;

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
        services.ConfigureSerilog(configuration);

        // RabbitMQ
        services.AddRabbitMq(configuration);
        services.AddSingleton<IRabbitMqProducer<SampleIntegrationEvent>, SampleProducer>();
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