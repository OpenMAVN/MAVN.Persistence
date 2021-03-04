using System.Threading.Tasks;

namespace MAVN.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Complete()
        {
            throw new System.NotImplementedException();
        }

        public Task CompleteAsync()
        {
            throw new System.NotImplementedException();
        }

        public IDataSet<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            throw new System.NotImplementedException();
        }
    }
}