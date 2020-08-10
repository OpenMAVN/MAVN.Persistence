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
    }
}
