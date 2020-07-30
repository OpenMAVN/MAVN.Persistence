using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence.EntityFrameworkCore.Tests
{
    internal class TestDbContext : EfDbContext
    {
        internal DbSet<TestEntity> Objects { get; set; }

        public TestDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnMAVNModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
