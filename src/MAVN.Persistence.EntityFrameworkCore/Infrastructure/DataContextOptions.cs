using System;

namespace MAVN.Persistence.Infrastructure
{
    internal sealed class DataContextOptions
    {
        private Type? _dataContextType;

        internal DbContextSettings DbContextSettings { get; }

        internal Type DataContextType
        {
            get => _dataContextType ?? throw new InvalidOperationException("DB context type is not set");
            set => _dataContextType = value;
        }

        internal DataContextOptions()
        {
            DbContextSettings = new DbContextSettings();
        }
    }
}