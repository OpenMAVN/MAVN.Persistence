using System;

namespace MAVN.Persistence
{
    public sealed class DbContextSettings
    {
        private const int DefaultTimeout = 30;

        private int? _commandTimeout;
        private string? _connectionString;
        private string? _schemaName;

        public int CommandTimeout
        {
            get => _commandTimeout ?? DefaultTimeout;
            set => _commandTimeout = value;
        }

        public string ConnectionString
        {
            get => _connectionString ?? throw new InvalidOperationException("DB connection string is not set");
            set => _connectionString = value;
        }

        public string SchemaName
        {
            get => _schemaName ?? throw new InvalidOperationException("DB schema is not set");
            set => _schemaName = value;
        }

        public string? MigrationsAssemblyName { get; set; }
    }
}