using System.Linq;
using MAVN.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    internal static class SpecificationEvaluator
    {
        internal static IQueryable<TEntity> Evaluate<TEntity>(
            this DbSet<TEntity> dbSet,
            ISpecification<TEntity>? specification)
            where TEntity : class
        {
            var query = dbSet.AsQueryable();

            if (specification == null)
                return query;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification.GroupBy != null)
            {
                query = query
                    .GroupBy(specification.GroupBy)
                    .SelectMany(x => x);
            }

            return query;
        }
    }
}