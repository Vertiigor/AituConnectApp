using RabbitMQ.Client;

namespace NotificationService.Connections.RabbitMq
{
    public interface IRabbitMqConnection
    {
        public Task<IConnection> GetConnectionAsync();
    }
}
