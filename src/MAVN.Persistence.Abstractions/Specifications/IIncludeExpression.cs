using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public interface IIncludeExpression<T>
    {
        IEnumerable<T> AddIncludes(IEnumerable<T> items);
    }
}
