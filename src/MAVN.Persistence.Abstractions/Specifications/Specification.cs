using JetBrains.Annotations;
using System;
using System.Linq.Expressions;

namespace MAVN.Persistence.Specifications
{
    public static class Specification
    {
        [PublicAPI]
        public static ISpecification<T> For<T>()
        {
            return new Specification<T>
            (
                criteria: null,
                groupBy: null
            );
        }
    }

    public sealed class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public Expression<Func<T, object>>? GroupBy { get; }

        internal Specification(
            Expression<Func<T, bool>>? criteria,
            Expression<Func<T, object>>? groupBy)
        {
            Criteria = criteria;
            GroupBy = groupBy;
        }
    }
}