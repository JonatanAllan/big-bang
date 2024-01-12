using Enterprise.Template.Core.RabbitMQ.Client;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Enterprise.Template.Application.Services.RabbitMQ
{
    public class SampleProducer : ProducerBase<SampleIntegrationEvent>
    {
        public SampleProducer(
            IConnectionFactory connectionFactory,
            ILogger<SampleProducer> logger) :
            base(connectionFactory, logger)
        {
        }

        protected override string ExchangeName => "CUSTOM_HOST.SampleExchange";
        protected override string RoutingKeyName => "sample.message";
        protected override string AppId => "SampleProducerId";
    }

    public class SampleIntegrationEvent(int entityId, string message, Guid? correlationId = null)
    {
        public Guid CorrelationId { get; set; } = correlationId?? Guid.NewGuid();
        public int EntityId { get; set; } = entityId;
        public string Message { get; set; } = message;
    }
}
