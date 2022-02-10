using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.InMemory")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.MsSql")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.PostgreSql")]
[assembly: InternalsVisibleTo("MAVN.Persistence.EntityFrameworkCore.MySql")]

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
            if (commandTimeout < 0)
                throw new ArgumentException();

            Options.DbContextSettings.CommandTimeout = commandTimeout;
            return this;
        }

        public DataContextOptionsBuilder WithRetriesCount(int retriesCount)
        {
            if (retriesCount < 1)
                throw new ArgumentException();

            Options.DbContextSettings.RetriesCount = retriesCount;
            return this;
        }

        public DataContextOptionsBuilder WithConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException();

            Options.DbContextSettings.ConnectionString = connectionString;
            return this;
        }

        public DataContextOptionsBuilder WithSchemaName(string schemaName)
        {
            if (string.IsNullOrWhiteSpace(schemaName))
                throw new ArgumentException();

            Options.DbContextSettings.SchemaName = schemaName;
            return this;
        }
    }
}