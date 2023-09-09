using Microsoft.Extensions.Caching.Distributed;

namespace Shop.Domain.Infra.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        public CachingService(IDistributedCache cache) 
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
                SlidingExpiration = TimeSpan.FromSeconds(60)
            };
        }

        public string Get(string key)
        {
            return _cache.GetString(key);
        }

        public void Set(string key, string value)
        {
            _cache.SetString(key, value, _options);
        }
    }
}
