using System;

namespace MAVN.Persistence.Infrastructure
{
    internal sealed class DataContextOptions
    {
        private Type? _dbContextType;
        private IDbContextOptionsConfigurator? _dbContextOptionsConfigurator;

        internal DbContextSettings DbContextSettings { get; }

        internal Type DbContextType
        {
            get => _dbContextType ?? throw new InvalidOperationException("DB context type is not set");
            set => _dbContextType = value;
        }

        internal IDbContextOptionsConfigurator DbContextOptionsConfigurator
        {
            get => _dbContextOptionsConfigurator ?? throw new InvalidOperationException("DB context options configurator is not set");
            set => _dbContextOptionsConfigurator = value;
        }

        internal DataContextOptions()
        {
            DbContextSettings = new DbContextSettings();
        }
    }
}