using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Distributed;
using Structure.Core.Entities;
using Structure.Infrastructure.Extensions;

namespace Structure.Infrastructure.Data
{
    public interface IExercisesRepository : IRepository<Core.Entities.Exercise>
    {
    }

    // If you wanted a non-cached version you could services.AddScoped<IExercisesRepository, ExercisesRepository>()
    public class ExercisesRepository : BaseRepository<Core.Entities.Exercise>, IExercisesRepository
    {
        public ExercisesRepository(Container container) : base(container)
        {
        }
    }

    public class CachedExercisesRepository : ExercisesRepository, IExercisesRepository
    {
        private readonly IDistributedCache _cache;

        public CachedExercisesRepository(Container container, IDistributedCache cache) : base(container)
        {
            _cache = cache;
        }

        public override async Task<Exercise?> GetByIdAsync(Guid id, string? partitionKey = null)
        {
            var cached = await _cache.GetAsync<Exercise>(ExerciseCacheKey(id));

            if (cached != null)
            {
                return cached;
            }

            return await base.GetByIdAsync(id);
        }

        public override async Task<Exercise> UpsertAsync(Exercise entity)
        {
            var res = await base.UpsertAsync(entity);

            var cacheOptions = new DistributedCacheEntryOptions();
            await _cache.SetAsync<Exercise>(ExerciseCacheKey(entity.Id), res, cacheOptions);

            return res;
        }

        public override async Task DeleteAsync(Exercise entity)
        {
            await _cache.RemoveAsync(ExerciseCacheKey(entity.Id));

            await base.DeleteAsync(entity);
        }

        private string ExerciseCacheKey(Guid id) => $"exercises:{id}";
    }
}