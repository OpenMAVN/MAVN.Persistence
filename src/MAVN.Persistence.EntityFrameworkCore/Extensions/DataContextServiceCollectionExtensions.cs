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

            serviceCollection.AddSingleton(typeof(IDbContextOptionsConfigurator), optionsBuilder.Options.DbContextOptionsConfiguratorType);

            serviceCollection.AddSingleton<IDbContextProvider>(x =>
                new DbContextProvider(
                    optionsBuilder.Options.DbContextType,
                    optionsBuilder.Options.DbContextSettings,
                    x.GetRequiredService<IDbContextOptionsConfigurator>()));

            serviceCollection.AddSingleton(typeof(IDataContext), typeof(DataContext));

            return serviceCollection;
        }
    }
}