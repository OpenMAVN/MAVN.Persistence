using Microsoft.Extensions.Logging;

namespace MAVN.Persistence
{
    internal sealed class DataContext : IDataContext
    {
        private readonly IDbContextProvider _dbContextProvider;
        private readonly ILoggerFactory? _loggerFatory;

        public DataContext(IDbContextProvider dbContextProvider)
            : this(dbContextProvider, null)
        {
        }

        public DataContext(
            IDbContextProvider dbContextProvider,
            ILoggerFactory? loggerFatory)
        {
            _dbContextProvider = dbContextProvider;
            _loggerFatory = loggerFatory;
        }

        public IUnitOfWork BeginUnitOfWork(bool withTransaction = false, bool enableLogging = false)
        {
            var dbContext = _dbContextProvider.CreateDbContext(enableLogging ? _loggerFatory : null);
            return new UnitOfWork(dbContext, withTransaction);
        }
    }
}