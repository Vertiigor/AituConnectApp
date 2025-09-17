using RabbitMQ.Client;

namespace AituConnectApi.Connections.RabbitMq
{
    public interface IRabbitMqConnection
    {
        public Task<IConnection> GetConnectionAsync();
    }
}
