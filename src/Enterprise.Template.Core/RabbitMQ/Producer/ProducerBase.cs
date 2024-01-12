using System.Text;
using Enterprise.Template.Core.RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Enterprise.Template.Core.RabbitMQ.Producer
{
    public abstract class ProducerBase<T> : RabbitMqClientBase, IRabbitMqProducer<T>
    {
        private readonly ILogger<ProducerBase<T>> _logger;
        protected abstract string ExchangeName { get; }
        protected abstract string RoutingKeyName { get; }
        protected abstract string AppId { get; }

        protected ProducerBase(
            IConnectionFactory connectionFactory,
            ILogger<ProducerBase<T>> logger) :
            base(connectionFactory, logger) => _logger = logger;

        public virtual void Publish(T @event)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
                if (Channel is null)
                    throw new NullReferenceException(nameof(Channel));

                var properties = Channel.CreateBasicProperties();
                properties.AppId = AppId;
                properties.ContentType = "application/json";
                properties.DeliveryMode = 1; // Doesn't persist to disk
                properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKeyName, body: body, basicProperties: properties);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error while publishing");
            }
        }
    }
}
