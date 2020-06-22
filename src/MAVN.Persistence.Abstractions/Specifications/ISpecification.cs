using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }

        Expression<Func<T, object>>? GroupBy { get; }

        PaginationExpression? TakePage { get; }
    }
}