using MAVN.Persistence.Infrastructure;

namespace MAVN.Persistence
{
    public static class DataContextOptionsBuilderExtensionsForDbContext
    {
        public static DataContextOptionsBuilder WithDbContext<TDbContext>(this DataContextOptionsBuilder builder)
            where TDbContext : EfDbContext
        {
            builder.Options.DbContextType = typeof(TDbContext);
            return builder;
        }
    }
}
