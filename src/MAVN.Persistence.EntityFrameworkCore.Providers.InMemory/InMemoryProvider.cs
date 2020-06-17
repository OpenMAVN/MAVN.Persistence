using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    [PublicAPI]
    public sealed class InMemoryProvider : DbContextProviderBase
    {
        public InMemoryProvider(
            Type dbContextType,
            DbContextSettings dbContextSettings) : base(dbContextType, dbContextSettings)
        {
        }

        protected override void OnPreConfiguring(
            DbContextOptionsBuilder optionsBuilder,
            DbContextSettings dbContextSettings)
        {
            optionsBuilder
                .UseInMemoryDatabase(dbContextSettings.SchemaName);
        }
    }
}