using CaliberFS.Template.Application.UseCases.SaveLog;
using CaliberFS.Template.Core.RabbitMQ.Client;
using CaliberFS.Template.Core.RabbitMQ.Consumer;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CaliberFS.Template.Consumer.Services
{
    public class LogConsumerService : ConsumerBaseService, IHostedService
    {
        protected const string VirtualHost = "CUSTOM_HOST";
        protected readonly string LoggerExchange = $"{VirtualHost}.LoggerExchange";
        protected sealed override string QueueName => $"{VirtualHost}.log.message";
        protected const string LoggerQueueAndExchangeRoutingKey = "log.message";

        public LogConsumerService(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<LogConsumerService> logConsumerLogger,
            ILogger<ConsumerBaseService> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(mediator, connectionFactory, consumerLogger, logger)
        {
            try
            {
                AddLogChannel();
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<SaveLogRequest>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logConsumerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        private void AddLogChannel()
        {
            Channel.ExchangeDeclare(
                exchange: LoggerExchange,
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
                exchange: LoggerExchange,
                routingKey: LoggerQueueAndExchangeRoutingKey);
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
