using System;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;
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
            serviceCollection.AddSingleton(typeof(IDataContext), optionsBuilder.Options.DbContextType);

            Type factoryType = typeof(DesignTimeDbContextFactory<>).MakeGenericType(optionsBuilder.Options.DbContextType);
            Type interfaceType = typeof(IDesignTimeDbContextFactory<>).MakeGenericType(optionsBuilder.Options.DbContextType);
            serviceCollection.AddSingleton(interfaceType, x =>
                Activator.CreateInstance(
                    factoryType,
                    optionsBuilder.Options.DbContextSettings,
                    x.GetRequiredService<IDbContextOptionsConfigurator>()));

            return serviceCollection;
        }
    }
}