using Enterprise.Logging.SDK.Configuration;
using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.Worker.Configuration;
using Enterprise.Template.Worker.Options;
using Enterprise.Template.Worker.Services;

namespace Enterprise.Template.Worker;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCustomHealthCheck(configuration);
        services.AddCustomSqlServer(configuration);
        services.AddApplications();
        services.Configure<PeriodicHostedServiceOptions>(configuration.GetSection("PeriodicService"));
        services.AddSingleton<PeriodicHostedService>();
        services.AddHostedService(
            provider => provider.GetRequiredService<PeriodicHostedService>());

        // RabbitMQ
        services.AddRabbitMq(configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.ConfigureLoggingMiddleware();

        app.UseRouting();
        app.UseHealthChecks();
        app.UseMapCustomEndpoints();
    }
}