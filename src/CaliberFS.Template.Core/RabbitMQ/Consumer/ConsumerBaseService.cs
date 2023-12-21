using System.Text;
using CaliberFS.Template.Core.RabbitMQ.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CaliberFS.Template.Core.RabbitMQ.Consumer
{
    public abstract class ConsumerBaseService : RabbitMqClientBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConsumerBaseService> _logger;
        protected abstract string QueueName { get; }

        protected ConsumerBaseService(
            IMediator mediator,
            IConnectionFactory connectionFactory,
            ILogger<ConsumerBaseService> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(connectionFactory, logger)
        {
            _mediator = mediator;
            _logger = consumerLogger;
        }

        protected virtual async Task OnEventReceived<T>(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var body = Encoding.UTF8.GetString(@event.Body.ToArray());
                var message = JsonConvert.DeserializeObject<T>(body)!;

                await _mediator.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error while retrieving message from queue.");
            }
            finally
            {
                Channel?.BasicAck(@event.DeliveryTag, false);
            }
        }
    }
}
