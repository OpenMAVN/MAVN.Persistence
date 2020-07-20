using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public sealed class OrderingExpression<T>
    {
        public OrderingDirection Direction { get; }

        public Expression<Func<T, object>> KeySelector { get; }

        internal OrderingExpression(
            OrderingDirection direction,
            Expression<Func<T, object>> keySelector)
        {
            Direction   = direction;
            KeySelector = keySelector;
        }

        public void Deconstruct(
            out OrderingDirection direction,
            out Expression<Func<T, object>> keySelector)
        {
            direction   = Direction;
            keySelector = KeySelector;
        }
    }
}