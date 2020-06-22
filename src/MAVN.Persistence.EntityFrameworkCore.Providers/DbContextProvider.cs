using System;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    public class DbContextProvider : IDbContextProvider
    {
        private readonly Type _dbContextType;
        private readonly DbContextSettings _dbContextSettings;
        private readonly IDbContextOptionsConfigurator _dbContextOptionsConfigurator;

        public DbContextProvider(
            Type dbContextType,
            DbContextSettings dbContextSettings,
            IDbContextOptionsConfigurator dbContextOptionsConfigurator)
        {
            if (dbContextType == null)
                throw new ArgumentNullException(nameof(dbContextType));

            if (!dbContextType.IsSubclassOf(typeof(DbContext)))
                throw new ArgumentException("Should be inherited from DbContext.", nameof(dbContextType));

            _dbContextType = dbContextType;
            _dbContextSettings = dbContextSettings ?? throw new ArgumentNullException(nameof(dbContextSettings));
            _dbContextOptionsConfigurator = dbContextOptionsConfigurator ?? throw new ArgumentNullException(nameof(dbContextOptionsConfigurator));
        }

        public DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            _dbContextOptionsConfigurator.Configure(optionsBuilder, _dbContextSettings);

            return (DbContext) Activator.CreateInstance(_dbContextType, optionsBuilder.Options);
        }
    }
}