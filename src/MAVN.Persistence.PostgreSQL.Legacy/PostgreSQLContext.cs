using System;
using System.Data.Common;
using System.Linq;
using JetBrains.Annotations;
using MAVN.Numerics;
using MAVN.Persistence.PostgreSQL.Legacy.Attributes;
using MAVN.Persistence.PostgreSQL.Legacy.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MAVN.Persistence.PostgreSQL.Legacy
{
    [PublicAPI]
    public abstract class PostgreSQLContext : DbContext
    {
        private const int DefaultCommandTimeout = 30;

        private readonly string _schema;
        private readonly bool _isForMocks;
        private readonly int? _commandTimeoutSeconds;
        private readonly DbConnection _dbConnection;

        private string _connectionString;

        public bool IsTraceEnabled { set; get; }

        /// <summary>
        /// Constructor used for migrations.
        /// </summary>
        protected PostgreSQLContext(string schema, int commandTimeoutSeconds = DefaultCommandTimeout)
        {
            _schema = schema;
            _commandTimeoutSeconds = commandTimeoutSeconds;
        }

        /// <summary>
        /// Constructor used for factory.
        /// </summary>
        protected PostgreSQLContext(
            string schema,
            string connectionString,
            bool isTraceEnabled,
            int commandTimeoutSeconds = DefaultCommandTimeout)
        {
            _schema = schema;
            _connectionString = connectionString;

            IsTraceEnabled = isTraceEnabled;

            _isForMocks = false;

            _commandTimeoutSeconds = commandTimeoutSeconds;
        }

        /// <summary>
        /// Constructor used for mocks.
        /// </summary>
        protected PostgreSQLContext(string schema, DbContextOptions contextOptions)
            : this(schema, contextOptions, true)
        {
        }

        /// <summary>
        /// Constructor used to customize db context options 
        /// </summary>
        protected PostgreSQLContext(
            string schema,
            DbContextOptions options,
            bool isForMocks = false,
            int commandTimeoutSeconds = DefaultCommandTimeout)
            : base(options)
        {
            _schema = schema;
            _isForMocks = isForMocks;
            _commandTimeoutSeconds = commandTimeoutSeconds;
        }

        /// <summary>
        /// Constructor used for factory with db connection and customization.
        /// </summary>
        protected PostgreSQLContext(
            string schema,
            DbConnection dbConnection,
            bool isForMocks = false,
            int commandTimeoutSeconds = DefaultCommandTimeout)
        {
            _schema = schema;
            _dbConnection = dbConnection;
        }

        protected virtual void OnMAVNConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_isForMocks)
                return;

            if (_dbConnection == null)
            {
                // Manual connection string entry for migrations.
                while (string.IsNullOrEmpty(_connectionString))
                {
                    Console.Write("Enter connection string: ");

                    _connectionString = Console.ReadLine();
                }
                optionsBuilder = optionsBuilder.UseNpgsql(_connectionString, options =>
                {
                    options
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, _schema)
                        .CommandTimeout(_commandTimeoutSeconds ?? DefaultCommandTimeout);
                });
            }
            else
            {
                optionsBuilder = optionsBuilder.UseNpgsql(_dbConnection, options =>
                {
                    options
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, _schema)
                        .CommandTimeout(_commandTimeoutSeconds ?? DefaultCommandTimeout);
                });
            }

#pragma warning disable 618
            
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            
#pragma warning restore 618

            OnMAVNConfiguring(optionsBuilder);

            base.OnConfiguring(optionsBuilder);
        }

        protected abstract void OnMAVNModelCreating(ModelBuilder modelBuilder);

        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties();

                var money18Properties = properties.Where(p => p.PropertyType == typeof(Money18));
                foreach (var property in money18Properties)
                {
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(
                            Attribute.IsDefined(property, typeof(Padding))
                                ? Money18PaddedConverter.Instance
                                : (ValueConverter)Money18Converter.Instance);
                }

                var nullableMoney18Properties = properties.Where(p => p.PropertyType == typeof(Money18?));
                foreach (var property in nullableMoney18Properties)
                {
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(
                            Attribute.IsDefined(property, typeof(Padding))
                            ? NullableMoney18PaddedConverter.Instance
                            : (ValueConverter)NullableMoney18Converter.Instance);
                }
            }

            OnMAVNModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
