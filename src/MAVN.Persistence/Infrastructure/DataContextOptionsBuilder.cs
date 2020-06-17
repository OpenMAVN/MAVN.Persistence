namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptionsBuilder
    {
        private DataContextOptions _options;

        public DataContextOptionsBuilder()
        {
            _options = new DataContextOptions();
        }

        public DataContextOptions Options => _options;
    }
}