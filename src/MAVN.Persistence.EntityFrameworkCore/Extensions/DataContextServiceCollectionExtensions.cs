using System;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MAVN.Persistence
{
    [PublicAPI]
    public static class DataContextServiceCollectionExtensions
    {
        public static IServiceCollection AddDataContext(
            this IServiceCollection serviceCollection,
            Action<DataContextOptionsBuilder> builderAction)
        {
            var optionsBuilder = new DataContextOptionsBuilder();

            builderAction.Invoke(optionsBuilder);

            // TODO: Validate DataContextOptions and DRY with Autofac

            serviceCollection
                .AddSingleton(typeof(IDataContext), optionsBuilder.Options.DataContextType);

            // TODO: Register additional dependencies from DataContextOptions and DRY with Autofac

            return serviceCollection;
        }
    }
}