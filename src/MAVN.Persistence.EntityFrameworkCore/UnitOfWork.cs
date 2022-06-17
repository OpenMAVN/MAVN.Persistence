using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace MAVN.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _dbContext;
        private readonly IDbContextTransaction? _dbContextTransaction;

        internal UnitOfWork(EfDbContext dbContext, bool withTransaction)
        {
            _dbContext = dbContext;
            if (withTransaction)
                _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
            _dbContext.Dispose();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
            _dbContextTransaction?.Commit();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
            if (_dbContextTransaction != null)
                await _dbContextTransaction.CommitAsync();
        }

        public IDataSet<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            return new DataSet<TEntity>(_dbContext);
        }
    }
}