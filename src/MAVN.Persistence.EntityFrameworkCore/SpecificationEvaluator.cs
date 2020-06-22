using System.Linq;
using MAVN.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Persistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> Evaluate<TEntity>(
            this DbSet<TEntity> dbSet,
            ISpecification<TEntity>? specification)
            where TEntity : class
        {
            var query = dbSet.AsQueryable();

            if (specification == null)
                return query;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification is ISpecificationWithOrdering<TEntity> specificationWithOrdering)
            {
                for (var i = 0; i < specificationWithOrdering.OrderBy.Count; i++)
                {
                    var (direction, keySelector) = specificationWithOrdering.OrderBy[i];

                    // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                    switch (direction)
                    {
                        case OrderingDirection.Ascending:
                            query = i == 0 
                                ? query.OrderBy(keySelector)
                                : ((IOrderedQueryable<TEntity>) query).ThenBy(keySelector);
                            break;
                        case OrderingDirection.Descending:
                            query = i == 0
                                ? query.OrderByDescending(keySelector)
                                : ((IOrderedQueryable<TEntity>) query).ThenByDescending(keySelector);
                            break;
                    }
                }
            }

            if (specification.GroupBy != null)
            {
                query = query
                    .GroupBy(specification.GroupBy)
                    .SelectMany(x => x);
            }

            // ReSharper disable once InvertIf
            if (specification.TakePage != null)
            {
                var (pageSize, pageIndex) = specification.TakePage;

                query = query
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize);
            }

            return query;
        }
    }
}