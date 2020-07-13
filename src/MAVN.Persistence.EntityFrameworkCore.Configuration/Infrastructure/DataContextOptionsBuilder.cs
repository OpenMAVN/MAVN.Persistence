using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.InMemory")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.MsSql")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.PostgreSql")]

namespace MAVN.Persistence.Infrastructure
{

    public sealed class DataContextOptionsBuilder
    {
        internal DataContextOptions Options { get; }

        public DataContextOptionsBuilder()
        {
            Options = new DataContextOptions();
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