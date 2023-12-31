﻿using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CaliberFS.Template.Core.RabbitMQ.Client
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        protected IModel? Channel { get; private set; }
        private IConnection? _connection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqClientBase> _logger;

        protected RabbitMqClientBase(
            IConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            ConnectToRabbitMq();
        }

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
                _connection = _connectionFactory.CreateConnection();

            if (Channel is { IsOpen: true }) return;
            Channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
            }
        }
    }
}
