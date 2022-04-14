using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Core.Entities;
using Structure.Infrastructure.Extensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;

namespace Structure.Infrastructure.Data
{
    public interface IRepository<T> where T: IEntity
    {
        public Task<T?> GetByIdAsync(Guid id, string? partitionKey = null);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        public Task<T> UpsertAsync(T entity);
        public Task DeleteAsync(T entity);
    }

    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        //private readonly 
        private Container _container;
        // private CosmosClient _client;
        public BaseRepository(Container container)
        {
            _container = container;
        }

        public async Task<T?> GetByIdAsync(Guid id, string? partitionKey = null)
        {
            if (partitionKey != null) 
            {
                try 
                {
                    return await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(partitionKey));
                }
                catch(CosmosException e)
                {
                    if (e.StatusCode == System.Net.HttpStatusCode.NotFound) 
                    {
                        return default(T);
                    }
                    throw;
                }                
            }

            return await _container.GetItemLinqQueryable<T>()
                .Where(e => e.Id == id)
                .ToFeedIterator()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null) 
            {
                predicate = (t) => true;
            }

            return await _container.GetItemLinqQueryable<T>()
                .Where(predicate!)
                .ToFeedIterator()
                .ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is ISoftDeleteEntity softDeleteEntity) 
            {
                softDeleteEntity.Deleted = true;
                softDeleteEntity.DeletedUtc = DateTime.UtcNow;
                await _container.UpsertItemAsync(softDeleteEntity, new PartitionKey(softDeleteEntity.PartitionKey));
            }
            else 
            {
                await this._container.DeleteItemAsync<T>(entity.Id.ToString(), new PartitionKey(entity.PartitionKey));
            }            
        }

        public async Task<T> UpsertAsync(T entity)
        {
            entity.UpdatedUtc = DateTime.UtcNow;
            return await _container.UpsertItemAsync<T>(entity, new PartitionKey(entity.PartitionKey));
        }
    }
}
