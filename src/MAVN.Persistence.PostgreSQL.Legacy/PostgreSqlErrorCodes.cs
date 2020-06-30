namespace MAVN.Persistence.PostgreSQL.Legacy
{    
    /// <summary>
    /// Postgre SQL  error codes
    /// </summary>
    public static class PostgreSqlErrorCodes
    {
        /// <summary>
        /// Cannot insert duplicate key row 
        /// </summary>
        public const int UniqueViolation = 23505;
    }
}