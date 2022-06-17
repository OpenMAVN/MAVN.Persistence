using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Persistence.Specifications;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MAVN.Persistence.EntityFrameworkCore.Tests
{
    public class InMemoryTests
    {
        [Fact]
        public async Task MultipleWhereConditionsTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataContext(c =>
            {
                c.UseEntityFrameworkInMemory()
                    .WithDbContext<TestDbContext>()
                    .WithSchemaName("tests");
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dataContext = serviceProvider.GetRequiredService<IDataContext>();
            using var uow = dataContext.BeginUnitOfWork();
            var dataSet = uow.DataSet<TestEntity>();
            var spec = Specification.For<TestEntity>()
                .Where(i => i.Id > 0)
                .Where(i => i.StrParam != null)
                .Where(i => i.IntParam == 0);

            var count = await dataSet.CountAsync(spec);

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task IncludeTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataContext(c =>
            {
                c.UseEntityFrameworkInMemory()
                    .WithDbContext<TestDbContext>()
                    .WithSchemaName("tests");
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dataContext = serviceProvider.GetRequiredService<IDataContext>();

            using var uow = dataContext.BeginUnitOfWork();
            var dataSet = uow.DataSet<TestEntity>();
            var testEntity = new TestEntity
            {
                Child = new TestChildEntity(),
                Children = new List<TestChildEntity>
                {
                    new TestChildEntity(),
                }
            };
            dataSet.Add(testEntity);
            await uow.CompleteAsync();

            using var uow2 = dataContext.BeginUnitOfWork();
            var dataSet2 = uow2.DataSet<TestEntity>();
            var fetchSpec = FetchSpecification.For<TestEntity>()
                .Include(i => i.Child)
                .Include(i => i.Children);

            var items = await dataSet2.FindAsync(null, fetchSpec);

            Assert.True(items.Count() > 0);
            Assert.NotNull(items.First().Child);
            Assert.True(items.First().Children.Count > 0);
        }

        [Fact]
        public async Task ThenIncludeTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataContext(c =>
            {
                c.UseEntityFrameworkInMemory()
                    .WithDbContext<TestDbContext>()
                    .WithSchemaName("tests");
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dataContext = serviceProvider.GetRequiredService<IDataContext>();

            using var uow = dataContext.BeginUnitOfWork();
            var dataSet = uow.DataSet<TestEntity>();
            var testEntity = new TestEntity
            {
                Child = new TestChildEntity
                {
                    GrandChildren = new List<TestGrandChildEntity>
                    {
                        new TestGrandChildEntity(),
                    }
                },
                Children = new List<TestChildEntity>
                {
                    new TestChildEntity
                    {
                        GrandChildren = new List<TestGrandChildEntity>()
                        {
                            new TestGrandChildEntity(),
                        }
                    },
                }
            };
            dataSet.Add(testEntity);
            await uow.CompleteAsync();

            using var uow2 = dataContext.BeginUnitOfWork();
            var dataSet2 = uow2.DataSet<TestEntity>();
            var fetchSpec = FetchSpecification.For<TestEntity>()
                .Include(i => i.Child)
                    .ThenInclude<TestEntity, TestChildEntity, ICollection<TestGrandChildEntity>>(i => i.GrandChildren);

            var items = await dataSet2.FindAsync(null, fetchSpec);

            Assert.True(items.Count() > 0);
            var first = items.First();
            Assert.NotNull(first.Child);
            Assert.True(first.Child.GrandChildren.Count > 0);

            fetchSpec = FetchSpecification.For<TestEntity>()
                .Include(i => i.Children)
                    .ThenInclude<TestEntity, TestChildEntity, ICollection<TestGrandChildEntity>>(i => i.GrandChildren);

            items = await dataSet2.FindAsync(null, fetchSpec);

            Assert.True(items.Count() > 0);
            first = items.First();
            Assert.True(first.Children.Count > 0);
            Assert.True(first.Children.First().GrandChildren.Count > 0);
        }
    }
}
