using AituConnectApi.Connections.Redis;
using AituConnectApi.Models.Redis;
using AituConnectApi.Services.Abstractions;
using StackExchange.Redis;
using System.Text.Json;

namespace AituConnectApi.Services.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IRedisConnection _connection;

        public CacheService(IRedisConnection connection)
        {
            _connection = connection;
        }

        public async Task<T?> GetAsync<T>(string key) where T : class, ICachable
        {
            var db = await GetDatabase();

            var json = await db.StringGetAsync(key) ;

            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(json!);
        }

        public async Task<IDatabase> GetDatabase()
        {
            var connection = await _connection.GetConnectionAsync();
            var db = connection.GetDatabase();
            return db;
        }

        public async Task SetAsync<T>(Cache<T> cache) where T : class, ICachable
        {
            var db = await GetDatabase();

            var json = JsonSerializer.Serialize(cache.Payload);

            await db.StringSetAsync(cache.Key, json, TimeSpan.FromHours(1));
        }

        public async Task RemoveAsync(string key)
        {
            var db = await GetDatabase();

            await db.KeyDeleteAsync(key);
        }
    }
}
