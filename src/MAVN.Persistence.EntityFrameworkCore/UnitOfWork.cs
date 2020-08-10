using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _dbContext;

        internal UnitOfWork(
            EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public IDataSet<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            return new DataSet<TEntity>(_dbContext);
        }
    }
}