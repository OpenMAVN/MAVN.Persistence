using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    public abstract class Specification
    {
        [PublicAPI]
        public static ISpecification<T> For<T>()
        {
            return new Specification<T>
            (
                criteria: null,
                groupBy:  null
            );
        }
    }
}