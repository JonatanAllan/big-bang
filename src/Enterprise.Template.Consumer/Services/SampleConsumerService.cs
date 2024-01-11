using System.Text;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Core.RabbitMQ.Client;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Enterprise.Template.Consumer.Services
{
    public class SampleConsumerService : RabbitMqClientBase, IHostedService
    {
        protected string VirtualHost => "CUSTOM_HOST";
        protected string SampleExchange => $"{VirtualHost}.SampleExchange";
        protected string QueueName => $"{VirtualHost}.sample.message";
        protected const string SampleQueueAndExchangeRoutingKey = "sample.message";
        private readonly IBoardApplication _boardApplication;
        private readonly ILogger<SampleConsumerService> _logger;

        public SampleConsumerService(
            IBoardApplication boardApplication,
            IConnectionFactory connectionFactory,
            ILogger<SampleConsumerService> logger,
            ILogger<RabbitMqClientBase> baseLogger) :
            base(connectionFactory, baseLogger)
        {
            _boardApplication = boardApplication;
            _logger = logger;
            try
            {
                AddSampleChannel();
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error while consuming message");
            }
        }

        private async Task OnEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var body = Encoding.UTF8.GetString(@event.Body.ToArray());
                var message = JsonConvert.DeserializeObject<HandleNewBoardRequest>(body)!;

                await _boardApplication.HandleNewBoard(message);
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
