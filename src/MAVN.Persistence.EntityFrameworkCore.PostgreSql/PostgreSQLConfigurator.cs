using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Persistence
{
    public sealed class PostgreSqlConfigurator : IDbContextOptionsConfigurator
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
                if (dbContextSettings.RetriesCount > 1)
                    options.EnableRetryOnFailure(dbContextSettings.RetriesCount);
                if (dbContextSettings.MigrationsAssemblyName != null)
                    options.MigrationsAssembly(dbContextSettings.MigrationsAssemblyName);
            });
        }
    }
}