using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Structure.Core.Entities;

namespace Structure.Infrastructure.Extensions
{
    public static class CosmosExtensions
    {
        public static async Task<T?> FirstOrDefaultAsync<T>(this FeedIterator<T> iterator) where T : IEntity
        {
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                
                return response.FirstOrDefault();
            }
            
            return default(T);
        }

        public static async Task<IEnumerable<T>> ToListAsync<T>(this FeedIterator<T> iterator) where T : IEntity
        {
            var results = new List<T>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                
                results.AddRange(response.ToList());
            }
            
            return results;
        }
    }
}
