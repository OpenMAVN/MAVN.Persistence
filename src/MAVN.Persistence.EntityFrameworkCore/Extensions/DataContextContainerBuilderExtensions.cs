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

            containerBuilder
                .RegisterType(optionsBuilder.Options.DbContextOptionsConfiguratorType)
                .As(typeof(IDbContextOptionsConfigurator))
                .SingleInstance();

            containerBuilder
                .Register(c =>
                {
                    var configurator = c.Resolve<IDbContextOptionsConfigurator>();
                    return new DbContextProvider(
                        optionsBuilder.Options.DbContextType,
                        optionsBuilder.Options.DbContextSettings,
                        configurator);
                })
                .As(typeof(IDbContextProvider))
                .SingleInstance();

            containerBuilder
                .RegisterType<DataContext>()
                .As(typeof(IDataContext))
                .SingleInstance();

            return containerBuilder;
        }
    }
}