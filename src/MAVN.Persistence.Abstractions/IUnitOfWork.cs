using System;
using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();

        Task CompleteAsync();

        void ExecuteWithTransaction(Action<Action> action);

        T ExecuteWithTransaction<T>(Func<Action, T> func);

        Task ExecuteWithTransactionAsync(Func<Func<Task>, Task> func);

        Task<T> ExecuteWithTransactionAsync<T>(Func<Func<Task>, Task<T>> func);

        IDataSet<TEntity> DataSet<TEntity>() 
            where TEntity : class;
    }
}