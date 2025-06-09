using AituConnectApi.Models.Redis;
using StackExchange.Redis;

namespace AituConnectApi.Services.Abstractions
{
    public interface ICacheService
    {
        public Task<T> GetAsync<T>(string key) where T : class, ICachable;
        public Task<IDatabase> GetDatabase();
        public Task SetAsync<T>(Cache<T> cache) where T : class, ICachable;
        public Task RemoveAsync(string key);
    }
}
