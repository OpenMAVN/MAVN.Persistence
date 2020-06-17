using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public interface ISpecificationWithOrdering<T> : ISpecification<T>
    {
        IReadOnlyList<OrderingExpression<T>> OrderBy { get; }
    }
}