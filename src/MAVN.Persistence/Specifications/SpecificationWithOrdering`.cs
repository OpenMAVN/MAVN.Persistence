using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace MAVN.Persistence.Specifications
{
    public sealed class SpecificationWithOrdering<T> : Specification, ISpecificationWithOrdering<T>
    {
        internal SpecificationWithOrdering(
            Expression<Func<T, bool>>? criteria,
            Expression<Func<T, object>>? groupBy,
            IEnumerable<OrderingExpression<T>>? orderBy,
            PaginationExpression? takePage)
        {
            Criteria = criteria;
            GroupBy = groupBy;
            OrderBy = orderBy?.ToImmutableArray() ?? ImmutableArray<OrderingExpression<T>>.Empty;
            TakePage = takePage;
        }
        
        
        public Expression<Func<T, bool>>? Criteria { get; }
        
        public Expression<Func<T, object>>? GroupBy { get; }
        
        public IReadOnlyList<OrderingExpression<T>> OrderBy { get; }
        
        public PaginationExpression? TakePage { get; }
    }
}