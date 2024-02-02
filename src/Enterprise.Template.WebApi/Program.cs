using Asp.Versioning.ApiExplorer;
using Enterprise.Configuration.Extensions;
using Enterprise.GenericRepository.Configuration;
using Enterprise.Logging.Models;
using Enterprise.Logging.SDK.Configuration;
using Enterprise.RabbitMQ.Models;
using Enterprise.Template.IoC;
using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.IoC.HealthChecks;
using Enterprise.Template.WebApi.Configuration;
using Enterprise.Template.WebApi.Infrastructure;
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
builder.Services.AddVersionedApi();
builder.Services.AddSwagger();
builder.Services.AddCustomHealthCheck(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddApplications();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// RabbitMQ
builder.Services.AddRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(_ => { });

app.UseRouting();

app.UseCors(ops => ops.AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapHealthChecks("/status", HealthCheckBase.GetHealthCheckOptions());

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseVersionedSwagger(provider);
app.MapControllers();

app.Run();

namespace Enterprise.Template.WebApi
{
    public partial class Program { }
}