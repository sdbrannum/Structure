using Structure.Core.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Structure.Infrastructure.Extensions;

namespace Structure.Infrastructure.Data
{
    public interface ICarsRepository : IRepository<Core.Entities.Car>
    {

    }

    public class CarsRepository : ICarsRepository
    {
        public CarsRepository()
        {

        }

        public virtual async Task<Car> GetByIdAsync(Guid id, string? partitionKey = null)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<Car>> GetAllAsync(Expression<Func<Car, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Car> UpsertAsync(Car entity)
        {
            throw new NotImplementedException();
        }
        public virtual async Task DeleteAsync(Car entity)
        {
            throw new NotImplementedException();
        }

    }

    public class CachedCarsRepository : CarsRepository
    {
        private readonly IDistributedCache _cache;
        public CachedCarsRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public override async Task<Car> GetByIdAsync(Guid id, string? partitionKey = null)
        {
            var cached = await _cache.GetAsync<Car>(CarCacheKey(id));

            if (cached != null)
            {
                return cached;
            }

            return await base.GetByIdAsync(id);
        }

        public override async Task<IEnumerable<Car>> GetAllAsync(Expression<Func<Car, bool>>? predicate = null)
        {
            throw new NotImplementedException();
            return await base.GetAllAsync(predicate);
        }


        public override async Task<Car> UpsertAsync(Car entity)
        {
            throw new NotImplementedException();
            return await base.UpsertAsync(entity); 
        }

        public override async Task DeleteAsync(Car entity)
        {
            throw new NotImplementedException();
            await base.DeleteAsync(entity);
        }

        private string CarCacheKey(Guid id) => $"cars:{id}";
    }
}
