namespace MAVN.Persistence
{
    public interface IDataContext
    {
        IUnitOfWork BeginUnitOfWork();
    }
}