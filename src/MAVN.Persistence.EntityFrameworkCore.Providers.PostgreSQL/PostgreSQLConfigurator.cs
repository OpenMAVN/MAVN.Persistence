using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Persistence
{
    [PublicAPI]
    public sealed class PostgreSQLConfigurator : IDbContextOptionsConfigurator
    {
        public void Configure(
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