using Enterprise.RabbitMQ.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Enterprise.Template.IoC.HealthChecks
{
    public class RabbitMQHealthCheck(IRabbitConnectionService connection) : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var operationResult = connection.HealthCheck();
            var rng = new Random();
            var channel = new QueueSample("simple_queue", rng.Next(0, 100) , 2);
            if (operationResult.Success)
            {
                return Task.FromResult(HealthCheckResult.Healthy(null, channel.ToDictionary()));
            }
            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, operationResult.Message));
        }
    }

    internal class QueueSample
    {
        public string Name { get; set; }
        public int Messages { get; set; }
        public int Consumers { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "name", Name },
                { "messages", Messages },
                { "consumers", Consumers }
            };
        }

        public QueueSample(string name, int messages, int consumers)
        {
            Name = name;
            Messages = messages;
            Consumers = consumers;
        }
    }
}
