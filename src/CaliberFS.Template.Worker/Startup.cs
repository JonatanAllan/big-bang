using CaliberFS.Template.Application.Services.RabbitMQ;
using CaliberFS.Template.Bootstrapper.DependencyInjection;
using CaliberFS.Template.Core.RabbitMQ.Producer;
using CaliberFS.Template.IoC.DependencyInjection;
using CaliberFS.Template.Worker.Configuration;
using CaliberFS.Template.Worker.Options;
using CaliberFS.Template.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CaliberFS.Template.Worker;

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
        services.ConfigureSerilog(configuration);

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