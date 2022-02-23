using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MAVN.Persistence.EntityFrameworkCore.MySql
{
    public class MySqlConfigurator : IDbContextOptionsConfigurator
    {
        public void Configure(
            DbContextOptionsBuilder optionsBuilder,
            DbContextSettings dbContextSettings)
        {
            optionsBuilder.UseMySql(dbContextSettings.ConnectionString, options =>
            {
                options
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbContextSettings.SchemaName)
                    .CommandTimeout(dbContextSettings.CommandTimeout);
                if (dbContextSettings.RetriesCount > 0)
                    options.EnableRetryOnFailure(dbContextSettings.RetriesCount);
                if (dbContextSettings.MigrationsAssemblyName != null)
                    options.MigrationsAssembly(dbContextSettings.MigrationsAssemblyName);
                options.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}_{entity}");
            });
        }
    }
}
