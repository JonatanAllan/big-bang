using CaliberFS.Template.Core.RabbitMQ.Client;
using CaliberFS.Template.Core.RabbitMQ.Producer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CaliberFS.Template.Application.Services.RabbitMQ
{
    public class SampleProducer : ProducerBase<SampleIntegrationEvent>
    {
        public SampleProducer(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<ProducerBase<SampleIntegrationEvent>> producerBaseLogger) :
            base(connectionFactory, logger, producerBaseLogger)
        {
        }

        protected override string ExchangeName => "CUSTOM_HOST.SampleExchange";
        protected override string RoutingKeyName => "sample.message";
        protected override string AppId => "SampleProducerId";
    }

    public class SampleIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
