using JetBrains.Annotations;
using System.Collections.Generic;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public interface IFetchSpecification<T>
    {
        IReadOnlyList<IIncludeExpression<T>> Include { get; }

        IReadOnlyList<OrderingExpression<T>> OrderBy { get; }

        PaginationExpression? TakePage { get; }
    }
}
