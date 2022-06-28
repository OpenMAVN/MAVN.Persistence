using System;
using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

       public IDataSet<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void ExecuteWithTransaction(Action<Action> action)
        {
            throw new NotImplementedException();
        }

        public T ExecuteWithTransaction<T>(Func<Action, T> func)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteWithTransactionAsync(Func<Func<Task>, Task> func)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteWithTransactionAsync<T>(Func<Func<Task>, Task<T>> func)
        {
            throw new NotImplementedException();
        }
    }
}