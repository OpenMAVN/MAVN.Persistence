using Microsoft.Extensions.Logging;

namespace MAVN.Persistence
{
    internal interface IDbContextProvider
    {
        EfDbContext CreateDbContext(ILoggerFactory? loggerFactory = null);
    }
}