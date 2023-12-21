using CaliberFS.Template.Consumer.Configuration;
using CaliberFS.Template.Consumer.Services;
using CaliberFS.Template.IoC.DependencyInjection;
using RabbitMQ.Client;

namespace CaliberFS.Template.Consumer;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCustomHealthCheck(configuration);
        services.AddCustomSqlServer(configuration);
        services.AddUseCases();

        services.AddHostedService<LogConsumerService>();
        services.AddRabbitMq(configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHealthChecks();
    }
}