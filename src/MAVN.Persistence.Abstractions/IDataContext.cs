namespace MAVN.Persistence
{
    public interface IDataContext
    {
        IUnitOfWork BeginUnitOfWork(bool withTransaction = false, bool enableLogging = false);
    }
}