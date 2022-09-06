using StackExchange.Redis;
using System.Text.Json;

namespace Reddis_NetCore6.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;
        private TimeSpan ExpireTime=>TimeSpan.FromDays(1);
        public CacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = _redisCon.GetDatabase();
        }
        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var endpoints = _redisCon.GetEndPoints(true);
            foreach(var endpoint in endpoints)
            {
                var server = _redisCon.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        public async Task<T> GetOrAdd<T>(string key, Func<Task<T>> action) where T : class
        {
            var result = await _cache.StringGetAsync(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(await action());
                await SetValue(key, result); 
            }
           
            return JsonSerializer.Deserialize<T>(result);
        }

        public T GetOrAdd<T>(string key, Func<T> action) where T : class
        {
            var result =_cache.StringGet(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(action());
                _cache.StringSet(key, result,ExpireTime);
            }
            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task<string> GetValue(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task<bool> SetValue(string key, string value)
        {
            return await _cache.StringSetAsync(key,value,ExpireTime);

        }
    }
}
