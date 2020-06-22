using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFrameworkWithPostgreSql(this DataContextOptionsBuilder builder)
        {
            builder.Options.DbContextOptionsConfiguratorType = typeof(PostgreSqlConfigurator);
            return builder;
        }
    }
}
