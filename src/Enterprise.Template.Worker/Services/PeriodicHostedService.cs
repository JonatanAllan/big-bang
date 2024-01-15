using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Worker.Options;
using Microsoft.Extensions.Options;

namespace Enterprise.Template.Worker.Services
{
    public record PeriodicHostedServiceState(bool IsEnabled, DateTime? LastExecution);

    internal class PeriodicHostedService(
        IServiceProvider serviceProvider,
        IOptions<PeriodicHostedServiceOptions> options,
        ILogger<PeriodicHostedService> logger) : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(options.Value.TimerInternalInSeconds);
        private int _executionCount;
        public bool IsEnabled { get; set; }
        public DateTime? LastExecution { get; private set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(_period);
            using var scope = serviceProvider.CreateScope();
            var samplePeriodicApplication = scope.ServiceProvider.GetRequiredService<ISamplePeriodicApplication>();
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        await samplePeriodicApplication.DoSomething();
                        _executionCount++;
                        logger.LogInformation(
                            $"Executed PeriodicHostedService - Count: {_executionCount}");
                        LastExecution = DateTime.UtcNow;
                    }
                    else
                    {
                        logger.LogInformation(
                            "Skipped PeriodicHostedService");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
