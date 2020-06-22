using System;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;

// ReSharper disable once CheckNamespace
namespace MAVN.Persistence
{
    [PublicAPI]
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseMongoDB(
            this DataContextOptionsBuilder builder)
        {
            throw new NotImplementedException();
        }

        public static DataContextOptionsBuilder WithConnectionString(
            this DataContextOptionsBuilder builder,
            string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}