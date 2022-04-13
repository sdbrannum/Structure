using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Core.Entities;
using Microsoft.Azure.Cosmos;

namespace Structure.Infrastructure.Data
{
    public interface IRepository<T> where T: IEntity
    {
        public Task<T> GetByIdAsync(Guid id, string? partitionKey = null);
        public Task<IEnumerable<T>> GetAllAsync(Func<T, bool>? predicate = null);
        public Task<T> UpsertAsync(T entity);
        public Task DeleteAsync(T entity);
    }

    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        //private readonly 

        public BaseRepository()
        {
        }

        public async Task<T> GetByIdAsync(Guid id, string? partitionKey = null)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool>? predicate = null)
        {
            throw new NotImplementedException();
        }

    }
}
