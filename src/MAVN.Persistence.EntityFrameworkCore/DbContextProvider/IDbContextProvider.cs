using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MAVN.Persistence
{
    public interface IDbContextProvider
    {
        EfDbContext CreateDbContext(ILoggerFactory? loggerFactory = null);
    }
}