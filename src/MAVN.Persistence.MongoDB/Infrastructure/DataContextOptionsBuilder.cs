using System;

namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptionsBuilder
    {
        private DataContextOptions _options;

        internal DataContextOptions Options => _options;

        public DataContextOptionsBuilder()
        {
            _options = new DataContextOptions();
        }

        public DataContextOptionsBuilder UseMongoDB()
        {
            throw new NotImplementedException();
        }

        public DataContextOptionsBuilder WithConnectionString(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}