namespace CaliberFS.Template.Core.RabbitMQ.Client
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Port { get; set; }
    }
}
