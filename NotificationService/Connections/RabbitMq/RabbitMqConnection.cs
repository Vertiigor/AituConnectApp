using Microsoft.Extensions.Options;
using NotificationService.Settings.RabbitMq;
using RabbitMQ.Client;

namespace NotificationService.Connections.RabbitMq
{
    public class RabbitMqConnection : IRabbitMqConnection, IDisposable
    {
        private readonly Task<IConnection> _connectionTask;
        private readonly RabbitMqSettings _settings;

        public RabbitMqConnection(IOptions<RabbitMqSettings> options)
        {
            _settings = options.Value;
            _connectionTask = InitializeConnectionAsync();
        }

        private async Task<IConnection> InitializeConnectionAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = $"{_settings.Host}",
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            return await factory.CreateConnectionAsync();
        }

        public Task<IConnection> GetConnectionAsync() => _connectionTask;

        public void Dispose()
        {
            if (_connectionTask.IsCompletedSuccessfully)
            {
                var connection = _connectionTask.Result;
                if (connection != null && connection.IsOpen)
                {
                    connection.Dispose();
                }
            }
        }
    }
}
