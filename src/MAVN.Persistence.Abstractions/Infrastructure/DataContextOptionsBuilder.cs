namespace MAVN.Persistence.Infrastructure
{
    public sealed class DataContextOptionsBuilder
    {
        private DataContextOptions _options;

        public DataContextOptions Options => _options;

        public DataContextOptionsBuilder()
        {
            _options = new DataContextOptions();
        }
    }
}