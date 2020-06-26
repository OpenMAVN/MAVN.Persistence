using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MAVN.Persistence
{
    internal class DesignTimeDbContextFactory<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : EfDbContext
    {
        private readonly DbContextSettings _dbContextSettings;
        private readonly IDbContextOptionsConfigurator _dbContextOptionsConfigurator;

        internal DesignTimeDbContextFactory(
            DbContextSettings dbContextSettings,
            IDbContextOptionsConfigurator dbContextOptionsConfigurator)
        {
            _dbContextSettings = dbContextSettings;
            _dbContextOptionsConfigurator = dbContextOptionsConfigurator;
        }

        public TDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            _dbContextOptionsConfigurator.Configure(optionsBuilder, _dbContextSettings);
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
        }
    }
}
