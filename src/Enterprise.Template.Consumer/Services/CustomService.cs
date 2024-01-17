using Enterprise.Logging.Models;
using Enterprise.PubSub.Interfaces;
using Enterprise.RabbitMQ.BaseClasses;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Domain.Constants;
using Microsoft.Extensions.Options;

namespace Enterprise.Template.Consumer.Services
{
    public class CustomService(
            IServiceProvider serviceProvider,
            ISubscriberService subscriberService,
            IOptions<LoggingSettings> loggingSettings,
            ILogger<WorkerBase> logger) : WorkerBase(subscriberService, loggingSettings, logger)
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeAsync(stoppingToken);
        }

        protected override void Subscribe()
        {
            using var scope = serviceProvider.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            Subscribe<HandleNewBoardRequest>(Queues.SampleMessage, boardApplication.HandleNewBoard);
        }
    }
}
