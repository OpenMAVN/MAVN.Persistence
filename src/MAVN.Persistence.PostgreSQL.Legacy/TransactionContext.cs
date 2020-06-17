using System.Data.Common;

namespace MAVN.Persistence.PostgreSQL.Legacy
{
    public class TransactionContext
    {
        internal DbConnection DbConnection { get; }

        public TransactionContext(DbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }
    }
}
