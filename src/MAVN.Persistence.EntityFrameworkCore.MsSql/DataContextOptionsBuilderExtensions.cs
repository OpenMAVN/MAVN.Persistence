using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFrameworkWithMsSql(this DataContextOptionsBuilder builder)
        {
            builder.Options.DbContextOptionsConfiguratorType = typeof(MsSqlConfigurator);
            return builder;
        }
    }
}
