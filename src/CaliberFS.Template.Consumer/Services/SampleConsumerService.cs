using CaliberFS.Template.Application.UseCases.HandleNewBoard;
using CaliberFS.Template.Core.RabbitMQ.Client;
using CaliberFS.Template.Core.RabbitMQ.Consumer;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CaliberFS.Template.Consumer.Services
{
    public class SampleConsumerService : ConsumerBaseService, IHostedService
    {
        protected const string VirtualHost = "CUSTOM_HOST";
        protected readonly string SampleExchange = $"{VirtualHost}.SampleExchange";
        protected sealed override string QueueName => $"{VirtualHost}.sample.message";
        protected const string SampleQueueAndExchangeRoutingKey = "sample.message";

        public SampleConsumerService(
            IMediator mediator,
            IConnectionFactory connectionFactory,
            ILogger<SampleConsumerService> sampleConsumerLogger,
            ILogger<ConsumerBaseService> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(mediator, connectionFactory, consumerLogger, logger)
        {
            try
            {
                AddSampleChannel();
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<HandleNewBoardRequest>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                sampleConsumerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        private void AddSampleChannel()
        {
            Channel.ExchangeDeclare(
                exchange: SampleExchange,
                type: "direct",
                durable: true,
                autoDelete: false);

            Channel.QueueDeclare(
                queue: QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false);

            Channel.QueueBind(
                queue: QueueName,
                exchange: SampleExchange,
                routingKey: SampleQueueAndExchangeRoutingKey);
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
