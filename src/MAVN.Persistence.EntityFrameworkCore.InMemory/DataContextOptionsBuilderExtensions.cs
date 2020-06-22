using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFrameworkInMemory(this DataContextOptionsBuilder builder)
        {
            builder.Options.DbContextOptionsConfiguratorType = typeof(InMemoryConfigurator);
            return builder;
        }
    }
}
