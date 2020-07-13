using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MAVN.Persistence
{
    public abstract class EfDbContext : DbContext
    {
        internal string? Schema { get; set; }
        public bool IsTraceEnabled { get; set; }

        public EfDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected virtual void OnMAVNConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO - replace with ILoggerFactory usage in DbContextProvider.CreateDbContext after migration of logging system to MS logging
            if (IsTraceEnabled)
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                optionsBuilder.UseLoggerFactory(loggerFactory);
            }

            OnMAVNConfiguring(optionsBuilder);

            base.OnConfiguring(optionsBuilder);
        }

        protected abstract void OnMAVNModelCreating(ModelBuilder modelBuilder);

        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (string.IsNullOrWhiteSpace(Schema))
                throw new InvalidOperationException($"{nameof(Schema)} must be set");
            modelBuilder.HasDefaultSchema(Schema);

            OnMAVNModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
