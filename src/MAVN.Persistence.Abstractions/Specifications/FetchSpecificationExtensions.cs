using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public static class FetchSpecificationExtensions
    {
        #region Ordering

        public static IFetchSpecification<T> OrderByAscending<T>(
            this IFetchSpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .OrderBy(OrderingDirection.Ascending, keySelector);
        }

        public static IFetchSpecification<T> OrderByDescending<T>(
            this IFetchSpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .OrderBy(OrderingDirection.Descending, keySelector);
        }

        public static IFetchSpecification<T> ThenByAscending<T>(
            this IFetchSpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .ThenBy(OrderingDirection.Ascending, keySelector);
        }

        public static IFetchSpecification<T> ThenByDescending<T>(
            this IFetchSpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .ThenBy(OrderingDirection.Descending, keySelector);
        }

        public static IFetchSpecification<T> WithNoOrder<T>(
            this IFetchSpecification<T> specification)
        {
            return new FetchSpecification<T>
            (
                include: specification.Include,
                orderBy: null,
                takePage: specification.TakePage
            );
        }

        private static IFetchSpecification<T> OrderBy<T>(
            this IFetchSpecification<T> specification,
            OrderingDirection direction,
            Expression<Func<T, object>> keySelector)
        {
            return new FetchSpecification<T>
            (
                include: specification.Include,
                orderBy: new[] { new OrderingExpression<T>(direction, keySelector), },
                takePage: specification.TakePage
            );
        }

        private static IFetchSpecification<T> ThenBy<T>(
            this IFetchSpecification<T> specification,
            OrderingDirection direction,
            Expression<Func<T, object>> keySelector)
        {
            var orderBy = specification.OrderBy.ToList();

            orderBy.Add(new OrderingExpression<T>(direction, keySelector));

            return new FetchSpecification<T>
            (
                include: specification.Include,
                orderBy: orderBy,
                takePage: specification.TakePage
            );
        }

        #endregion

        #region Pagination

        public static IFetchSpecification<T> TakePage<T>(
            this IFetchSpecification<T> specification,
            int pageNumber,
            int pageSize)
        {
            return specification
                .TakePage(new PaginationExpression(pageSize, pageNumber));
        }

        public static IFetchSpecification<T> TakeAll<T>(
            this IFetchSpecification<T> specification)
        {
            return specification
                .TakePage(null);
        }

        private static IFetchSpecification<T> TakePage<T>(
            this IFetchSpecification<T> specification,
            PaginationExpression? takePage)
        {
            return new FetchSpecification<T>
            (
                include: specification.Include,
                orderBy: specification.OrderBy,
                takePage: takePage
            );
        }

        #endregion
    }
}
