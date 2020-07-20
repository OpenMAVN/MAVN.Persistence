using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MAVN.Persistence.Specifications
{
    public static class IncludeExpressionExtensions
    {
        #region Include

        public static IFetchSpecification<T> Include<T, TProperty>(
            this IFetchSpecification<T> specification,
            Expression<Func<T, TProperty>> keySelector)
        {
            var includes = new List<IIncludeExpression<T>>();
            if (specification.Include.Any())
                includes.AddRange(specification.Include);
            var includeExpr = new IncludeExpression<T>();
            includeExpr.Include<T, TProperty>(keySelector);
            includes.Add(includeExpr);
            return new FetchSpecification<T>
            (
                include: includes,
                orderBy: specification.OrderBy,
                takePage: specification.TakePage
            );
        }

        public static IFetchSpecification<T> ThenInclude<T, TPreviousProperty, TProperty>(
            this IFetchSpecification<T> specification,
            Expression<Func<TPreviousProperty, TProperty>> keySelector)
        {
            var lastInclude = specification.Include.Last();
            ((IncludeExpression<T>)lastInclude).ThenInclude(keySelector);
            return specification;
        }

        #endregion
    }
}
