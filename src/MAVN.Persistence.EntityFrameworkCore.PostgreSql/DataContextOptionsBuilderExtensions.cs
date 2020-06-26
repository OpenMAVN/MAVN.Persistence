using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFrameworkWithPostgreSql(
            this DataContextOptionsBuilder builder,
            string? migrationsAsssemblyName = null)
        {
            builder.Options.DbContextOptionsConfiguratorType = typeof(PostgreSqlConfigurator);
            builder.Options.DbContextSettings.MigrationsAssemblyName = migrationsAsssemblyName;
            return builder;
        }
    }
}
