namespace Enterprise.Template.Core.RabbitMQ.Producer
{
    public interface IRabbitMqProducer<in T>
    {
        void Publish(T @event);
    }
}
