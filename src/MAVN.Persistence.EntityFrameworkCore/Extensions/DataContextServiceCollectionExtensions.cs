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

            var dbContextProvider = new DbContextProvider(
                optionsBuilder.Options.DbContextType,
                optionsBuilder.Options.DbContextSettings,
                optionsBuilder.Options.DbContextOptionsConfigurator);
            serviceCollection
                .AddSingleton(typeof(IDbContextProvider), dbContextProvider);
            serviceCollection
                .AddSingleton(typeof(IDataContext), optionsBuilder.Options.DbContextType);

            return serviceCollection;
        }
    }
}