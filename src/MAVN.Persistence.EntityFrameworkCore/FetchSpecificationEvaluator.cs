using System.Linq;
using MAVN.Persistence.Specifications;

namespace MAVN.Persistence
{
    internal static class FetchSpecificationEvaluator
    {
        internal static IQueryable<TEntity> EvaluateFetch<TEntity>(
            this IQueryable<TEntity> query,
            IFetchSpecification<TEntity>? specification)
            where TEntity : class
        {
            if (specification == null)
                return query;

            foreach (var includeExpression in specification.Include)
            {
                query = (IQueryable<TEntity>)includeExpression.AddIncludes(query);
            }

            for (var i = 0; i < specification.OrderBy.Count; i++)
            {
                var (direction, keySelector) = specification.OrderBy[i];

                switch (direction)
                {
                    case OrderingDirection.Ascending:
                        query = i == 0
                            ? query.OrderBy(keySelector)
                            : ((IOrderedQueryable<TEntity>)query).ThenBy(keySelector);
                        break;
                    case OrderingDirection.Descending:
                        query = i == 0
                            ? query.OrderByDescending(keySelector)
                            : ((IOrderedQueryable<TEntity>)query).ThenByDescending(keySelector);
                        break;
                }
            }

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
