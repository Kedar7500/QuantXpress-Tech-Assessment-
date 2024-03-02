using StackExchange.Redis;
using System.Text.Json;

namespace CachingWebAPI.Services
{
    public class CacheService : ICacheService
    {
        private readonly  IDatabase _cacheDb;

        private readonly ILogger<CacheService> _logger;

        public CacheService(ILogger<CacheService> logger)
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb=redis.GetDatabase();
            _logger = logger;

        }
        public T GetData<T>(string key)
        {
            _logger.LogInformation("Checking cache for key: {CacheKey}", key);

            var value= _cacheDb.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                _logger.LogInformation("Cache hit for key: {CacheKey}", key);
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public object RemoveData(string key)
        {
            var _exist=_cacheDb.KeyExists(key); 
            if(_exist)
            {
                return _cacheDb.KeyDelete(key);
            }
            return false;
        }

       
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key,JsonSerializer.Serialize(value), expiryTime);

        }

      
        public bool IsKeyCached(string key)
        {
            return _cacheDb.KeyExists(key);
        }
    }
}
