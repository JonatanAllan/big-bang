using Enterprise.Template.Application.Services.RabbitMQ;
using Enterprise.Template.Consumer.Configuration;
using Enterprise.Template.Consumer.Services;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Enterprise.Template.IoC.DependencyInjection;
using RabbitMQ.Client;

namespace Enterprise.Template.Consumer;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCustomHealthCheck(configuration);
        services.AddCustomSqlServer(configuration);
        services.AddUseCases();
        services.AddHostedService<SampleConsumerService>();

        // RabbitMQ
        services.AddRabbitMq(configuration);
        services.AddSingleton<IRabbitMqProducer<SampleIntegrationEvent>, SampleProducer>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHealthChecks();
    }
}