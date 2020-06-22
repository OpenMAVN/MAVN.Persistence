using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptionsBuilder
    {
        public DataContextOptions Options { get; }

        public DataContextOptionsBuilder()
        {
            Options = new DataContextOptions();
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