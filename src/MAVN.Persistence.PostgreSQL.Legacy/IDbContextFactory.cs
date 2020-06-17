using JetBrains.Annotations;

namespace MAVN.Persistence.PostgreSQL.Legacy
{
    [PublicAPI]
    public interface IDbContextFactory<out T>
    {
        T CreateDataContext();

        T CreateDataContext(TransactionContext transactionContext);
    }
}