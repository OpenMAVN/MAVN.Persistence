using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence.EntityFrameworkCore.Tests
{
    internal class TestDbContext : EfDbContext
    {
        internal DbSet<TestEntity> Objects { get; set; }
        internal DbSet<TestChildEntity> Children { get; set; }
        internal DbSet<TestGrandChildEntity> GrandChildren { get; set; }

        public TestDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnMAVNModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TestEntity>();
            entity.HasOne(i => i.Child);
            entity.HasMany(i => i.Children);

            var childEntity = modelBuilder.Entity<TestChildEntity>();
            childEntity.HasMany(i => i.GrandChildren);
        }
    }
}
