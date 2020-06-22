using System;
using Autofac;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    [PublicAPI]
    public static class DataContextContainerBuilderExtensions
    {
        public static ContainerBuilder AddDataContext(
            this ContainerBuilder containerBuilder,
            Action<DataContextOptionsBuilder> builderAction)
        {
            var optionsBuilder = new DataContextOptionsBuilder();

            builderAction.Invoke(optionsBuilder);

            var dbContextProvider = new DbContextProvider(
                optionsBuilder.Options.DbContextType,
                optionsBuilder.Options.DbContextSettings,
                optionsBuilder.Options.DbContextOptionsConfigurator);
            containerBuilder
                .RegisterInstance(dbContextProvider)
                .As(typeof(IDbContextProvider))
                .SingleInstance();
            containerBuilder
                .RegisterType(optionsBuilder.Options.DbContextType)
                .As(typeof(IDataContext))
                .SingleInstance();

            return containerBuilder;
        }
    }
}