using StackExchange.Redis;

namespace AituConnectApi.Connections.Redis
{
    public interface IRedisConnection
    {
        Task<IConnectionMultiplexer> GetConnectionAsync();
    }
}
