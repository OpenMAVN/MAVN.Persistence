using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptionsBuilder
    {
        internal DataContextOptions Options { get; }

        internal DataContextOptionsBuilder()
        {
            Options = new DataContextOptions();
        }

        public DataContextOptionsBuilder UseEntityFrameworkInMemory()
        {
            Options.DbContextOptionsConfigurator = null; // implement dynamic load for InMemory configurator
            return this;
        }

        public DataContextOptionsBuilder UseEntityFrameworkWithPostgreSql()
        {
            Options.DbContextOptionsConfigurator = null; // implement dynamic load for PostgreSql configurator
            return this;
        }

        public DataContextOptionsBuilder UseEntityFrameworkWithMsSql()
        {
            Options.DbContextOptionsConfigurator = null; // implement dynamic load for MsSql configurator
            return this;
        }

        public DataContextOptionsBuilder WithDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            Options.DbContextType = typeof(TDbContext);
            return this;
        }

        public DataContextOptionsBuilder WithCommandTimeout(int commandTimeout)
        {
            Options.DbContextSettings.CommandTimeout = commandTimeout;
            return this;
        }

        public DataContextOptionsBuilder WithConnectionString(string connectionString)
        {
            Options.DbContextSettings.ConnectionString = connectionString;
            return this;
        }

        public DataContextOptionsBuilder WithSchemaName(string schemaName)
        {
            Options.DbContextSettings.SchemaName = schemaName;
            return this;
        }
    }
}