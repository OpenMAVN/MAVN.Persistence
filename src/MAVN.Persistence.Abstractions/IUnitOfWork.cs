using System;
using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        void Complete();

        Task SaveChangesAsync();

        Task CompleteAsync();

        IDataSet<TEntity> DataSet<TEntity>() 
            where TEntity : class;
    }
}