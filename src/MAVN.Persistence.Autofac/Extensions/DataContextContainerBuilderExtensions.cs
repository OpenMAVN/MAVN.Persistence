using System;
using Autofac;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;

// ReSharper disable once CheckNamespace
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

            // TODO: Validate DataContextOptions and DRY with Microsoft.Extensions.DependencyInjection

            containerBuilder
                .RegisterType(optionsBuilder.Options.ContextType)
                .As(typeof(IDataContext));

            // TODO: Register additional dependencies from DataContextOptions and DRY with Microsoft.Extensions.DependencyInjection
            
            return containerBuilder;
        }
    }
}