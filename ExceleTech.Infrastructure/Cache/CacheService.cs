using Microsoft.Extensions.Caching.Distributed;
using JsonNet.ContractResolvers;
using System.Text;
using Newtonsoft.Json;
using ExceleTech.Domain.Interfaces.Services;

namespace ExceleTech.Infrastructure.Cache
{
    public class CacheService<T> : ICacheService<T> where T : class
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }   

        public async Task<T> GetDataAsync(string key)
        {
           string response = await _cache.GetStringAsync(key);
           if (!string.IsNullOrEmpty(response)) 
           {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new PrivateSetterContractResolver()
                };
                T result = JsonConvert.DeserializeObject<T>(response, settings);
                return result;
                
           }
           return default(T);
        }

        public async Task RemoveDataAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetDataAsync(string key, T value, TimeSpan ExpirationTime)
        {
            var CacheData = JsonConvert.SerializeObject(value);
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(CacheData),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = ExpirationTime
                    });
        }
    }
}
