using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace MAVN.Persistence.Specifications
{
    [PublicAPI]
    public sealed class OrderingExpression<T>
    {
        public OrderingExpression(
            OrderingDirection direction,
            Expression<Func<T, object>> keySelector)
        {
            Direction   = direction;
            KeySelector = keySelector;
        }

        
        public OrderingDirection Direction { get; }

        public Expression<Func<T, object>> KeySelector { get; }


        public void Deconstruct(
            out OrderingDirection direction,
            out Expression<Func<T, object>> keySelector)
        {
            direction   = Direction;
            keySelector = KeySelector;
        }
    }
}