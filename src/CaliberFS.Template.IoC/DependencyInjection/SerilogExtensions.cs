using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaliberFS.Template.Core.RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;
using LogLevel = Microsoft.Identity.Client.LogLevel;

namespace CaliberFS.Template.Bootstrapper.DependencyInjection
{
    public static class SerilogExtensions
    {
        public static IServiceCollection ConfigureSerilog(this IServiceCollection services, IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                 .SetLogLevel()
                 .Enrich()
                 .WriteToConsole()
                 .WriteToFile()
                 //.WriteToRabbitMq(config)
                 .CreateLogger();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();
            services.AddSingleton<ILoggerFactory>(loggerFactory);

            return services;
        }

        private static LoggerConfiguration SetLogLevel(this LoggerConfiguration loggerConfig)
        {
            loggerConfig
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

            return loggerConfig;
        }

        private static LoggerConfiguration Enrich(this LoggerConfiguration loggerConfig)
        {
            var currentProcess = Process.GetCurrentProcess();

            return loggerConfig
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty("ProcessID", currentProcess.Id)
                .Enrich.WithProperty("ProcessName", currentProcess.ProcessName);
        }

        private static LoggerConfiguration WriteToConsole(this LoggerConfiguration loggerConfig)
        {
            return loggerConfig.WriteTo.Console(
                               outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
        }

        private static LoggerConfiguration WriteToFile(this LoggerConfiguration loggerConfig)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log-.txt");
            return loggerConfig.WriteTo.File(
                path: filePath,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 7,
                encoding: Encoding.UTF8);
        }

        private static LoggerConfiguration WriteToRabbitMq(this LoggerConfiguration loggerConfig, IConfiguration configuration)
        {
            var options = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>()!;
            return loggerConfig.WriteTo.RabbitMQ((clientConfig, sinkConfig) =>
            {
                clientConfig.Port = options.Port;
                clientConfig.Username = options.UserName;
                clientConfig.Password = options.Password;
                clientConfig.Exchange = "logs";
                clientConfig.RouteKey = "logs";
                clientConfig.ExchangeType = "fanout";
                clientConfig.DeliveryMode = RabbitMQDeliveryMode.Durable;
                clientConfig.Hostnames.Add(options.HostName);
                sinkConfig.TextFormatter = new JsonFormatter();
            });
        }
    }
}
