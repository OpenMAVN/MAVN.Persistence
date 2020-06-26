using System;
using Autofac;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

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
                .RegisterType<DbContextProvider>()
                .As(typeof(IDbContextProvider))
                .SingleInstance()
                .WithParameter(TypedParameter.From(optionsBuilder.Options.DbContextType))
                .WithParameter(TypedParameter.From(optionsBuilder.Options.DbContextSettings));
            containerBuilder
                .RegisterType(optionsBuilder.Options.DbContextType)
                .As(typeof(IDataContext))
                .SingleInstance();

            Type factoryType = typeof(DesignTimeDbContextFactory<>).MakeGenericType(optionsBuilder.Options.DbContextType);
            Type interfaceType = typeof(IDesignTimeDbContextFactory<>).MakeGenericType(optionsBuilder.Options.DbContextType);
            containerBuilder.RegisterType(factoryType)
                .As(interfaceType)
                .SingleInstance()
                .WithParameter(TypedParameter.From(optionsBuilder.Options.DbContextSettings));

            return containerBuilder;
        }
    }
}