using AituConnectApi.Settings.Redis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace AituConnectApi.Connections.Redis
{
    public class RedisConnection : IRedisConnection, IDisposable
    {
        private readonly Task<IConnectionMultiplexer> _connection;
        private readonly RedisSettings _settings;

        public RedisConnection(IOptions<RedisSettings> options)
        {
            _settings = options.Value;
            _connection = InitializeConnectionAsync();
        }

        private async Task<IConnectionMultiplexer> InitializeConnectionAsync()
        {
            var configuration = new ConfigurationOptions
            {
                EndPoints = { $"{_settings.Host}:{_settings.Port}" },
                Password = _settings.Password,
                AbortOnConnectFail = false,
                ConnectRetry = _settings.ConnectRetry,
                ConnectTimeout = _settings.ConnectTimeout
            };

            return await ConnectionMultiplexer.ConnectAsync(configuration);
        }

        public Task<IConnectionMultiplexer> GetConnectionAsync() => _connection;

        public void Dispose()
        {
            if (_connection.IsCompleted && _connection.IsCompletedSuccessfully)
            {
                _connection.Result?.Dispose();
            }
        }
    }
}
