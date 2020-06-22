using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    [PublicAPI]
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