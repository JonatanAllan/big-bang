using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CaliberFS.Template.Bootstrapper.DependencyInjection
{
    public static class SerilogExtensions
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
        {
            var logger = new LoggerConfiguration()
                 .SetLogLevel()
                 .Enrich()
                 .WriteToConsole()
                 .WriteToFile()
                .CreateLogger();

            builder.UseSerilog(logger);

            return builder;
        }

        private static LoggerConfiguration SetLogLevel(this LoggerConfiguration configuration)
        {
            configuration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

            return configuration;
        }

        private static LoggerConfiguration Enrich(this LoggerConfiguration configuration)
        {
            var currentProcess = Process.GetCurrentProcess();

            return configuration
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty("ProcessID", currentProcess.Id)
                .Enrich.WithProperty("ProcessName", currentProcess.ProcessName);
        }

        private static LoggerConfiguration WriteToConsole(this LoggerConfiguration configuration)
        {
            return configuration.WriteTo.Console(
                               outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
        }

        private static LoggerConfiguration WriteToFile(this LoggerConfiguration configuration)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log-.txt");
            return configuration.WriteTo.File(
                path: filePath,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 7,
                encoding: Encoding.UTF8);
        }
    }
}
