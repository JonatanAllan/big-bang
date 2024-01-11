using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Enterprise.Template.Core.RabbitMQ;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Enterprise.Template.WebApi.Configuration;
using Enterprise.Template.Application.Services.RabbitMQ;

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
        services.AddUseCases();
        services.AddExceptionHandler<CustomExceptionHandler>();

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