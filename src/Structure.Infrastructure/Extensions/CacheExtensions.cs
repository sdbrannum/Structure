using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string cacheKey, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                throw new ArgumentNullException(cacheKey);
            }

            var bytes = await cache.GetAsync(cacheKey, cancellationToken);

            return bytes.FromByteArray<T>();

        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options,  CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            await cache.SetAsync(key, value.ToByteArray(), options, cancellationToken);
        }
    }

    public static class Serialization
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return default(T);
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(byteArray);
        }

    }
}
