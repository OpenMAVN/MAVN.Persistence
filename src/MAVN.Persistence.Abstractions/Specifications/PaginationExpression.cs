using System;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public sealed class PaginationExpression
    {
        public int PageSize { get; }

        public int PageIndex { get; }

        public PaginationExpression(
            int pageSize,
            int pageIndex)
        {
            if (pageSize <= 0)
                throw new ArgumentException("Should be greater that zero.", nameof(pageSize));

            if (pageIndex < 0)
                throw new ArgumentException("Should be greater or equal to zero.", nameof(pageIndex));

            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public void Deconstruct(
            out int pageSize,
            out int pageIndex)
        {
            pageSize  = PageSize;
            pageIndex = PageIndex;
        }
    }
}