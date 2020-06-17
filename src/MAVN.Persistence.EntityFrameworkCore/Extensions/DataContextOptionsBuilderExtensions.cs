using System;
using JetBrains.Annotations;
using MAVN.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace MAVN.Persistence
{
    [PublicAPI]
    public static class DataContextOptionsBuilderExtensions
    {
        public static DataContextOptionsBuilder UseEntityFramework(
            this DataContextOptionsBuilder builder,
            string provider)
        {
            throw new NotImplementedException();
        }
        
        public static DataContextOptionsBuilder UseEntityFramework<TProvider>(
            this DataContextOptionsBuilder builder)
            where TProvider : IDbContextProvider
        {
            throw new NotImplementedException();
        }
        
        public static DataContextOptionsBuilder WithDbContext<TDbContext>(
            this DataContextOptionsBuilder builder)
            where TDbContext : DbContext
        {
            throw new NotImplementedException();
        } 
        
        public static DataContextOptionsBuilder WithCommandTimeout(
            this DataContextOptionsBuilder builder,
            int commandTimeout)
        {
            throw new NotImplementedException();
        }
        
        public static DataContextOptionsBuilder WithConnectionString(
            this DataContextOptionsBuilder builder,
            string connectionString)
        {
            throw new NotImplementedException();
        }
        
        public static DataContextOptionsBuilder WithSchemaName(
            this DataContextOptionsBuilder builder,
            string schemaName)
        {
            throw new NotImplementedException();
        }
    }
}