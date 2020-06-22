using System;

namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptions
    {
        private Type? _dbContextType;
        private Type? _dbContextOptionsConfiguratorType;

        public DbContextSettings DbContextSettings { get; }

        public Type DbContextType
        {
            get => _dbContextType ?? throw new InvalidOperationException("DB context type is not set");
            set => _dbContextType = value;
        }

        public Type DbContextOptionsConfiguratorType
        {
            get => _dbContextOptionsConfiguratorType ?? throw new InvalidOperationException("DB context options configurator type is not set");
            set => _dbContextOptionsConfiguratorType = value;
        }

        internal DataContextOptions()
        {
            DbContextSettings = new DbContextSettings();
        }
    }
}