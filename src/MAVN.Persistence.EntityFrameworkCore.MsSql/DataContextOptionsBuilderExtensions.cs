using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFrameworkWithMsSql(
            this DataContextOptionsBuilder builder,
            string? migrationsAsssemblyName = null)
        {
            builder.Options.DbContextOptionsConfiguratorType = typeof(MsSqlConfigurator);
            builder.Options.DbContextSettings.MigrationsAssemblyName = migrationsAsssemblyName;
            return builder;
        }
    }
}
