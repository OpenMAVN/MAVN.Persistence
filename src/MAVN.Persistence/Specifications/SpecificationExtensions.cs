using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public static class SpecificationExtensions
    {
        #region Criteria
        
        public static ISpecification<T> Where<T>(
            this ISpecification<T> specification,
            Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>
            (
                criteria: predicate,
                groupBy:  specification.GroupBy,
                takePage: specification.TakePage
            );
        }
        
        public static ISpecificationWithOrdering<T> Where<T>(
            this ISpecificationWithOrdering<T> specification,
            Expression<Func<T, bool>> predicate)
        {
            return new SpecificationWithOrdering<T>
            (
                criteria: predicate,
                groupBy:  specification.GroupBy,
                orderBy:  specification.OrderBy,
                takePage: specification.TakePage
            );
        }
        
        public static ISpecification<T> WhereAny<T>(
            this ISpecification<T> specification)
        {
            return new Specification<T>
            (
                criteria: null,
                groupBy:  specification.GroupBy,
                takePage: specification.TakePage
            );
        }
        
        public static ISpecificationWithOrdering<T> WhereAny<T>(
            this ISpecificationWithOrdering<T> specification)
        {
            return new SpecificationWithOrdering<T>
            (
                criteria: null,
                groupBy:  specification.GroupBy,
                orderBy:  specification.OrderBy,
                takePage: specification.TakePage
            );
        }
        
        #endregion
        
        #region Grouping
        
        public static ISpecification<T> GroupBy<T>(
            this ISpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return new Specification<T>
            (
                criteria: specification.Criteria,
                groupBy:  keySelector,
                takePage: specification.TakePage
            );
        }
        
        public static ISpecificationWithOrdering<T> GroupBy<T>(
            this ISpecificationWithOrdering<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return new SpecificationWithOrdering<T>
            (
                criteria: specification.Criteria,
                groupBy:  keySelector,
                orderBy:  specification.OrderBy,
                takePage: specification.TakePage
            );
        }
        
        public static ISpecification<T> WithNoGrouping<T>(
            this ISpecification<T> specification)
        {
            return new Specification<T>
            (
                criteria: specification.Criteria,
                groupBy:  null,
                takePage: specification.TakePage
            );
        }
        
        public static ISpecificationWithOrdering<T> WithNoGrouping<T>(
            this ISpecificationWithOrdering<T> specification)
        {
            return new SpecificationWithOrdering<T>
            (
                criteria: specification.Criteria,
                groupBy:  null,
                orderBy:  specification.OrderBy,
                takePage: specification.TakePage
            );
        }
        
        #endregion
        
        #region Ordering
        
        public static ISpecificationWithOrdering<T> OrderByAscending<T>(
            this ISpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .OrderBy(OrderingDirection.Ascending, keySelector);
        }
        
        public static ISpecificationWithOrdering<T> OrderByDescending<T>(
            this ISpecification<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .OrderBy(OrderingDirection.Descending, keySelector);
        }

        public static ISpecificationWithOrdering<T> ThenByAscending<T>(
            this ISpecificationWithOrdering<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .ThenBy(OrderingDirection.Ascending, keySelector);
        }

        public static ISpecificationWithOrdering<T> ThenByDescending<T>(
            this ISpecificationWithOrdering<T> specification,
            Expression<Func<T, object>> keySelector)
        {
            return specification
                .ThenBy(OrderingDirection.Descending, keySelector);
        }

        public static ISpecification<T> WithNoOrder<T>(
            this ISpecificationWithOrdering<T> specification)
        {
            return new Specification<T>
            (
                criteria: specification.Criteria,
                groupBy:  specification.GroupBy,
                takePage: specification.TakePage
            );
        }

        private static ISpecificationWithOrdering<T> OrderBy<T>(
            this ISpecification<T> specification,
            OrderingDirection direction,
            Expression<Func<T, object>> keySelector)
        {
            return new SpecificationWithOrdering<T>
            (
                criteria: specification.Criteria,
                groupBy:  specification.GroupBy,
                orderBy:  new [] { new OrderingExpression<T>(direction, keySelector), },
                takePage: specification.TakePage
            );
        }

        private static ISpecificationWithOrdering<T> ThenBy<T>(
            this ISpecificationWithOrdering<T> specification,
            OrderingDirection direction,
            Expression<Func<T, object>> keySelector)
        {
            var orderBy = specification.OrderBy.ToList();
            
            orderBy.Add(new OrderingExpression<T>(direction, keySelector));
            
            return new SpecificationWithOrdering<T>
            (
                criteria: specification.Criteria,
                groupBy:  specification.GroupBy,
                orderBy:  orderBy,
                takePage: specification.TakePage
            );
        }

        #endregion

        #region Pagination

        public static ISpecification<T> TakePage<T>(
            this ISpecification<T> specification,
            int pageNumber,
            int pageSize)
        {
            return specification
                .TakePage(new PaginationExpression(pageSize, pageNumber));
        }
        
        public static ISpecificationWithOrdering<T> TakePage<T>(
            this ISpecificationWithOrdering<T> specification,
            int pageNumber,
            int pageSize)
        {
            return specification
                .TakePage(new PaginationExpression(pageSize, pageNumber));
        }
        
        public static ISpecification<T> TakeAll<T>(
            this ISpecification<T> specification)
        {
            return specification
                .TakePage(null);
        }
        
        public static ISpecificationWithOrdering<T> TakeAll<T>(
            this ISpecificationWithOrdering<T> specification)
        {
            return specification
                .TakePage(null);
        }
        
        private static ISpecification<T> TakePage<T>(
            this ISpecification<T> specification,
            PaginationExpression? takePage)
        {
            return new Specification<T>
            (
                criteria: specification.Criteria,
                groupBy:  specification.GroupBy,
                takePage: takePage
            );
        }
        
        private static ISpecificationWithOrdering<T> TakePage<T>(
            this ISpecificationWithOrdering<T> specification,
            PaginationExpression? takePage)
        {
            return new SpecificationWithOrdering<T>
            (
                criteria: specification.Criteria,
                groupBy:  specification.GroupBy,
                orderBy:  specification.OrderBy,
                takePage: takePage
            );
        }
        
        #endregion
    }
}