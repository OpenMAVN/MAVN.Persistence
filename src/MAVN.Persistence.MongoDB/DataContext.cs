namespace MAVN.Persistence
{
    public class DataContext : IDataContext
    {
        public IUnitOfWork BeginUnitOfWork(bool withTransaction = false, bool enableLogging = false)
        {
            throw new System.NotImplementedException();
        }
    }
}