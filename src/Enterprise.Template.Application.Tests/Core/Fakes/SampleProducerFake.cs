using Enterprise.Template.Application.Services.RabbitMQ;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Enterprise.Template.Application.Tests.Core.Fakes
{
    public class SampleProducerFake : IRabbitMqProducer<SampleIntegrationEvent>
    {
        public readonly Dictionary<string, SampleIntegrationEvent> Events = new();
        public SampleProducerFake()
        {
        }

        public string ExchangeName => "CUSTOM_HOST.SampleExchange";
        public string RoutingKeyName => "sample.message";
        public string AppId => "SampleProducerId";

        public void Publish(SampleIntegrationEvent @event)
        {
            Events.Add(@event.CorrelationId.ToString(), @event);
        }
    }
}
