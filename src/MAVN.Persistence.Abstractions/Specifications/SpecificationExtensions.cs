using System;
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
            var criteria = predicate;
            if (specification.Criteria != null)
            {
                var body = Expression.AndAlso(specification.Criteria.Body, predicate.Body);
                criteria = Expression.Lambda<Func<T, bool>>(body, predicate.Parameters[0]);
            }
            return new Specification<T>
            (
                criteria: criteria,
                groupBy:  specification.GroupBy
            );
        }

        public static ISpecification<T> WhereAny<T>(
            this ISpecification<T> specification)
        {
            return new Specification<T>
            (
                criteria: null,
                groupBy:  specification.GroupBy
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
                groupBy:  keySelector
            );
        }

        public static ISpecification<T> WithNoGrouping<T>(
            this ISpecification<T> specification)
        {
            return new Specification<T>
            (
                criteria: specification.Criteria,
                groupBy:  null
            );
        }

        #endregion
    }
}