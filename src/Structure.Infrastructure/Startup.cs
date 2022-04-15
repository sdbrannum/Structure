using Structure.Infrastructure.Pipeline;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Azure.Cosmos;
using Structure.Infrastructure.Data;

namespace Structure.Infrastructure
{
    public static class Startup
    {
        public static void AddInfrastructure(this IServiceCollection serviceCollection, string dbName, string dbConnectionString)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            serviceCollection.AddMediatR(executingAssembly);
            serviceCollection.AddValidatorsFromAssembly(executingAssembly);
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            serviceCollection.AddRepositories(dbName, dbConnectionString);
        }

        private static void AddRepositories(this IServiceCollection serviceCollection, string dbName, string dbConnectionString)
        {
            var cosmosClient = new CosmosClient(dbConnectionString);
            var db = cosmosClient.GetDatabase(dbName);

            serviceCollection.AddSingleton<CosmosContainers>(new CosmosContainers(db));
            serviceCollection.AddSingleton<IExercisesRepository, ExercisesRepository>();
        }
    }

    public class CosmosContainers
    {
        public const string ExercisesContainerId = "exercises";

        private Database _db;

        public CosmosContainers(Database db)
        {
            _db = db;
        }

        public Container GetExercisesContainer(Database db)
        {
            return db.GetContainer(ExercisesContainerId);
        }
    }
}