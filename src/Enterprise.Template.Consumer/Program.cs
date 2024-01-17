using Enterprise.Configuration.Extensions;
using Enterprise.GenericRepository.Configuration;
using Enterprise.Logging.Models;
using Enterprise.Logging.SDK.Configuration;
using Enterprise.RabbitMQ.Models;
using Enterprise.Template.Consumer.Configuration;
using Enterprise.Template.Consumer.Services;
using Enterprise.Template.IoC;
using Enterprise.Template.IoC.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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
builder.Services.AddHostedService<CustomService>();

// RabbitMQ
builder.Services.AddRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.ConfigureLoggingMiddleware();

app.MapHealthChecks("/status", new HealthCheckOptions
{
    ResponseWriter = async (httpContext, result) =>
    {
        httpContext.Response.ContentType = "application/json";

        var json = new JObject(
            new JProperty("status", result.Status.ToString()),
            new JProperty("results", new JObject(result.Entries.Select(pair =>
                new JProperty(pair.Key, new JObject(
                    new JProperty("status", pair.Value.Status.ToString()),
                    new JProperty("description", pair.Value.Description),
                    new JProperty("data", new JObject(pair.Value.Data.Select(
                        p => new JProperty(p.Key, p.Value))))))))));
        await httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
    }
});

app.Run();
