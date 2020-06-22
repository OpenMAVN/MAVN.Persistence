using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    public sealed class InMemoryConfigurator : IDbContextOptionsConfigurator
    {
        public void Configure(
            DbContextOptionsBuilder optionsBuilder,
            DbContextSettings dbContextSettings)
        {
            optionsBuilder
                .UseInMemoryDatabase(dbContextSettings.SchemaName);
        }
    }
}