using Enterprise.Template.Application.Services.RabbitMQ;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.Worker.Configuration;
using Enterprise.Template.Worker.Options;
using Enterprise.Template.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Enterprise.Template.Worker;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCustomHealthCheck(configuration);
        services.AddCustomSqlServer(configuration);
        services.AddUseCases();

        services.Configure<PeriodicHostedServiceOptions>(configuration.GetSection("PeriodicService"));
        services.AddSingleton<PeriodicHostedService>();
        services.AddHostedService(
            provider => provider.GetRequiredService<PeriodicHostedService>());

        // RabbitMQ
        services.AddRabbitMq(configuration);
        services.AddSingleton<IRabbitMqProducer<SampleIntegrationEvent>, SampleProducer>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseHealthChecks();
        app.UseMapCustomEndpoints();
    }
}