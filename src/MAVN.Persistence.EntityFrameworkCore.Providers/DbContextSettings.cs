namespace MAVN.Persistence
{
    public sealed class DbContextSettings
    {
        public DbContextSettings(
            string schemaName,
            string connectionString,
            int commandTimeout)
        {
            CommandTimeout = commandTimeout;
            ConnectionString = connectionString;
            SchemaName = schemaName;
        }

        
        public int CommandTimeout { get; }
        
        public string ConnectionString { get; }
        
        public string SchemaName { get; }
        
        
        public void Deconstruct(
            out int commandTimeout,
            out string connectionString,
            out string schemaName)
        {
            commandTimeout = CommandTimeout;
            connectionString = ConnectionString;
            schemaName = SchemaName;
        }
    }
}