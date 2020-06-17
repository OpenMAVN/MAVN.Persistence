using System;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    public abstract class DbContextProviderBase : IDbContextProvider
    {
        private readonly DbContextSettings _dbContextSettings;
        private readonly Type _dbContextType;
        
        
        protected DbContextProviderBase(
            Type dbContextType,
            DbContextSettings dbContextSettings)
        {
            if (!dbContextType.IsSubclassOf(typeof(DbContext)))
            {
                throw new ArgumentException("Should be inherited from DbContext.", nameof(dbContextType));
            }
            
            _dbContextSettings = dbContextSettings;
            _dbContextType = dbContextType;
        }
        
        public DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            OnPreConfiguring(optionsBuilder, _dbContextSettings);
            
            return (DbContext) Activator.CreateInstance(_dbContextType, optionsBuilder);
        }
        
        protected abstract void OnPreConfiguring(
            DbContextOptionsBuilder optionsBuilder,
            DbContextSettings dbContextSettings);
    }
}