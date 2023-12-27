using StackExchange.Redis;
using System.Text.Json;

namespace Assesment.Services.Cache
{
    public class CacheService : ICacheService
    {
        IDatabase _cacheDb;
        private readonly string redisPort = "localhost:6379";
        public CacheService()
        {

            var redis = ConnectionMultiplexer.Connect(redisPort);

            _cacheDb = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public object RemoveData(string key)
        {
            var _exist = _cacheDb.KeyExists(key);
            if (_exist)
            {
                return _cacheDb.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);

        }
    }
}
