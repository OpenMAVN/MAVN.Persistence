using Microsoft.Extensions.Logging;

namespace MAVN.Persistence
{
    /// <summary>
    /// Used by design time factories to create DbContext.
    /// </summary>
    public interface IDbContextProvider
    {
        EfDbContext CreateDbContext(ILoggerFactory? loggerFactory = null);
    }
}