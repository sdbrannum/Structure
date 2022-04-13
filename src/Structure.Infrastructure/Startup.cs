using Structure.Infrastructure.Pipeline;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Structure.Infrastructure
{
    public static class Startup
    {
        public static void UseInfrastructure(this IServiceCollection serviceCollection)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();            

            serviceCollection.AddMediatR(executingAssembly);
            serviceCollection.AddValidatorsFromAssembly(executingAssembly);
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
