using System;
using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();

        IDataSet<TEntity> DataSet<TEntity>() 
            where TEntity : class;
    }
}