namespace MAVN.Persistence
{
    public sealed class DataContext : IDataContext
    {
        private readonly IDbContextProvider _dbContextProvider;

        public DataContext(
            IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public IUnitOfWork BeginUnitOfWork()
        {
            return new UnitOfWork(_dbContextProvider.CreateDbContext());
        }
    }
}