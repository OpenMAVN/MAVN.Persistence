using System;
using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _dbContext;

        internal UnitOfWork(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void ExecuteWithTransaction(Action<Action> action)
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();
            executionStrategy.Execute<object?, object?>(
                state: null,
                operation: (ctx, state) =>
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            action(() => _dbContext.SaveChanges());

                            transaction.Commit();

                            return null;
                        }
                        catch
                        {
                            transaction.Rollback();

                            throw;
                        }
                    }
                },
                verifySucceeded: null);
        }

        public T ExecuteWithTransaction<T>(Func<Action, T> action)
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();
            return executionStrategy.Execute<object?, T>(
                state: null,
                operation: (ctx, state) =>
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = action(() => _dbContext.SaveChanges());

                            transaction.Commit();

                            return result;
                        }
                        catch
                        {
                            transaction.Rollback();

                            throw;
                        }
                    }
                },
                verifySucceeded: null);
        }

        public async Task ExecuteWithTransactionAsync(Func<Func<Task>, Task> func)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync<object?, object?>(
                state: null,
                operation: async (ctx, state, ct) =>
                {
                    await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    await func(async () => await _dbContext.SaveChangesAsync());
                    await transaction.CommitAsync();
                    return null;
                },
                verifySucceeded: null);
        }

        public async Task<T> ExecuteWithTransactionAsync<T>(Func<Func<Task>, Task<T>> func)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<object?, T>(
                state: null,
                operation: async (ctx, state, ct) =>
                {
                    await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    var result = await func(async () => await _dbContext.SaveChangesAsync());
                    await transaction.CommitAsync();
                    return result;
                },
                verifySucceeded: null);
        }

        public IDataSet<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            return new DataSet<TEntity>(_dbContext);
        }
    }
}