using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Persistence
{
    [PublicAPI]
    public sealed class PostgreSQLProvider : DbContextProviderBase
    {
        public PostgreSQLProvider(
            Type dbContextType,
            DbContextSettings dbContextSettings) : base(dbContextType, dbContextSettings)
        {
        }

        protected override void OnPreConfiguring(
            DbContextOptionsBuilder optionsBuilder,
            DbContextSettings dbContextSettings)
        {
            optionsBuilder.UseNpgsql(dbContextSettings.ConnectionString, options =>
            {
                options
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbContextSettings.SchemaName)
                    .CommandTimeout(dbContextSettings.CommandTimeout);
            });
        }
    }
}