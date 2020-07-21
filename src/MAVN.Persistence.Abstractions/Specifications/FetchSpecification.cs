using JetBrains.Annotations;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MAVN.Persistence.Specifications
{
    public static class FetchSpecification
    {
        [PublicAPI]
        public static IFetchSpecification<T> For<T>()
        {
            return new FetchSpecification<T>
            (
                include: null,
                orderBy: null,
                takePage: null
            );
        }
    }

    public sealed class FetchSpecification<T> : IFetchSpecification<T>
    {
        public IReadOnlyList<IIncludeExpression<T>> Include { get; }

        public IReadOnlyList<OrderingExpression<T>> OrderBy { get; }

        public PaginationExpression? TakePage { get; }

        public FetchSpecification(
            IEnumerable<IIncludeExpression<T>>? include,
            IEnumerable<OrderingExpression<T>>? orderBy,
            PaginationExpression? takePage)
        {
            Include = include?.ToImmutableArray() ?? ImmutableArray<IIncludeExpression<T>>.Empty;
            OrderBy = orderBy?.ToImmutableArray() ?? ImmutableArray<OrderingExpression<T>>.Empty;
            TakePage = takePage;
        }
    }
}