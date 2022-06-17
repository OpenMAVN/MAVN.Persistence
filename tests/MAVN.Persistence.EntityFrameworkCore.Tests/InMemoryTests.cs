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
        private IDataContext _dataContext;

        public InMemoryTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataContext(c =>
            {
                c.UseEntityFrameworkInMemory()
                    .WithDbContext<TestDbContext>()
                    .WithSchemaName("tests");
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _dataContext = serviceProvider.GetRequiredService<IDataContext>();
        }

        [Fact]
        public async Task MultipleWhereConditionsTest()
        {
            using var uow = _dataContext.BeginUnitOfWork();
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
            using var uow = _dataContext.BeginUnitOfWork();
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

            using var uow2 = _dataContext.BeginUnitOfWork();
            var dataSet2 = uow2.DataSet<TestEntity>();
            var spec = Specification.For<TestEntity>()
                .Where(i => i.Id == testEntity.Id);
            var fetchSpec = FetchSpecification.For<TestEntity>()
                .Include(i => i.Child)
                .Include(i => i.Children);

            var items = await dataSet2.FindAsync(spec, fetchSpec);

            Assert.True(items.Count() > 0);
            Assert.NotNull(items.First().Child);
            Assert.True(items.First().Children.Count > 0);
        }

        [Fact]
        public async Task IncludeSingleThenIncludeTest()
        {
            using var uow = _dataContext.BeginUnitOfWork();
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
            };
            dataSet.Add(testEntity);
            await uow.CompleteAsync();

            using var uow2 = _dataContext.BeginUnitOfWork();
            var dataSet2 = uow2.DataSet<TestEntity>();
            var spec = Specification.For<TestEntity>()
                .Where(i => i.Id == testEntity.Id);
            var fetchSpec = FetchSpecification.For<TestEntity>()
                .Include(i => i.Child)
                    .ThenInclude<TestEntity, TestChildEntity, ICollection<TestGrandChildEntity>>(i => i.GrandChildren);

            var items = await dataSet2.FindAsync(spec, fetchSpec);

            Assert.True(items.Count() > 0);
            var first = items.First();
            Assert.NotNull(first.Child);
            Assert.True(first.Child.GrandChildren.Count > 0);
        }

        [Fact]
        public async Task IncludeCollectionThenIncludeTest()
        {
            using var uow = _dataContext.BeginUnitOfWork();
            var dataSet = uow.DataSet<TestEntity>();
            var testEntity = new TestEntity
            {
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

            using var uow2 = _dataContext.BeginUnitOfWork();
            var dataSet2 = uow2.DataSet<TestEntity>();
            var spec = Specification.For<TestEntity>()
                .Where(i => i.Id == testEntity.Id);
            var fetchSpec = FetchSpecification.For<TestEntity>()
                .Include(i => i.Children)
                    .ThenInclude<TestEntity, TestChildEntity, ICollection<TestGrandChildEntity>>(i => i.GrandChildren);

            var items = await dataSet2.FindAsync(spec, fetchSpec);

            Assert.True(items.Count() > 0);
            var first = items.First();
            Assert.True(first.Children.Count > 0);
            Assert.True(first.Children.First().GrandChildren.Count > 0);
        }
    }
}
