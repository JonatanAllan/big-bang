using Enterprise.Configuration.Extensions;
using Enterprise.Logging.Models;
using Enterprise.Logging.SDK.Configuration;
using Enterprise.RabbitMQ.Models;
using Enterprise.Template.IoC.DependencyInjection;
using Serilog;

namespace Enterprise.Template.WebApi;

public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddConfiguration<RabbitMQSettings>(hostContext, "RabbitMQSettings");
            services.AddLoggingSDK(hostContext);
        })
        .UseSerilog((hostContext, services, loggerConfiguration) =>
        {
            var loggingSettings = hostContext.GetConfigurationSection<LoggingSettings>("LoggingSettings");
            services.SerilogLoggingConfiguration(hostContext, loggerConfiguration);
        })
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}