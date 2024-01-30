using Enterprise.Configuration.Extensions;
using Enterprise.GenericRepository.Configuration;
using Enterprise.Logging.Models;
using Enterprise.Logging.SDK.Configuration;
using Enterprise.RabbitMQ.Models;
using Enterprise.Template.IoC;
using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.IoC.HealthChecks;
using Enterprise.Template.Worker.Configuration;
using Enterprise.Template.Worker.Options;
using Enterprise.Template.Worker.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureServices((hostContext, services) =>
    {
        services.AddConfiguration<AppSettings>(hostContext, "AppSettings");
        services.AddConfiguration<RabbitMQSettings>(hostContext, "RabbitMQSettings");
        services.AddLoggingSDK(hostContext);

        var appSettings = hostContext.GetConfigurationSection<AppSettings>(nameof(AppSettings));
        services.ConfigureDatabaseConnections(appSettings.ConnectionStrings);

    })
    .UseSerilog((hostContext, services, loggerConfiguration) =>
    {
        var loggingSettings = hostContext.GetConfigurationSection<LoggingSettings>("LoggingSettings");
        services.SerilogLoggingConfiguration(hostContext, loggerConfiguration);
    });

builder.Services.AddControllers();
builder.Services.AddCustomHealthCheck(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddApplications();
builder.Services.Configure<PeriodicHostedServiceOptions>(builder.Configuration.GetSection("PeriodicService"));
builder.Services.AddSingleton<PeriodicHostedService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicHostedService>());


// RabbitMQ
builder.Services.AddRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.ConfigureLoggingMiddleware();

app.MapHealthChecks("/status", HealthCheckBase.GetHealthCheckOptions());

app.MapGet("/background", (
    [FromServices] PeriodicHostedService service) => new PeriodicHostedServiceState(service.IsEnabled, service.LastExecution));

app.MapMethods("/background", new[] { "PATCH" }, (
    [FromBody] PeriodicHostedServiceState state,
    [FromServices] PeriodicHostedService service) =>
{
    service.IsEnabled = state.IsEnabled;
});

app.Run();
