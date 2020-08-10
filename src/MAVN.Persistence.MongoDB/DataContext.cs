namespace MAVN.Persistence
{
    public class DataContext : IDataContext
    {
        public IUnitOfWork BeginUnitOfWork(bool enableLogging = false)
        {
            throw new System.NotImplementedException();
        }
    }
}