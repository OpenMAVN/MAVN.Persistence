using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    public interface IDbContextOptionsConfigurator
    {
        void Configure(
            DbContextOptionsBuilder optionsBuilder,
            DbContextSettings dbContextSettings);
    }
}
