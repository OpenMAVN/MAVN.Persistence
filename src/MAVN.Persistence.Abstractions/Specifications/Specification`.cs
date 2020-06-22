using System;
using System.Linq.Expressions;

namespace MAVN.Persistence.Specifications
{
    public sealed class Specification<T> : Specification, ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public Expression<Func<T, object>>? GroupBy { get; }

        public PaginationExpression? TakePage { get; }

        internal Specification(
            Expression<Func<T, bool>>? criteria,
            Expression<Func<T, object>>? groupBy,
            PaginationExpression? takePage)
        {
            Criteria = criteria;
            GroupBy = groupBy;
            TakePage = takePage;
        }
    }
}