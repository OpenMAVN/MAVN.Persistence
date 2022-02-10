using MAVN.Persistence.EntityFrameworkCore.MySql;
using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFrameworkWithMySql(
            this DataContextOptionsBuilder builder,
            string? migrationsAsssemblyName = null)
        {
            builder.Options.DbContextOptionsConfiguratorType = typeof(MySqlConfigurator);
            builder.Options.DbContextSettings.MigrationsAssemblyName = migrationsAsssemblyName;
            return builder;
        }
    }
}
