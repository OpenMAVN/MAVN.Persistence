using Microsoft.EntityFrameworkCore;
using System;

namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptionsBuilder
    {
        internal DataContextOptions Options { get; }

        internal DataContextOptionsBuilder()
        {
            Options = new DataContextOptions();
        }

        public DataContextOptionsBuilder UseEntityFramework(string provider)
        {
            throw new NotImplementedException();
        }

        public DataContextOptionsBuilder UseEntityFrameworkInMemory()
        {
            throw new NotImplementedException();
        }

        public DataContextOptionsBuilder WithDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            throw new NotImplementedException();
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