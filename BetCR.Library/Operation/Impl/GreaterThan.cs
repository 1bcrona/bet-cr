
using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "greater than" comparison.
    /// </summary>
    public class GreaterThan : OperationBase
    {
        /// <inheritdoc />
        public GreaterThan() : base("GreaterThan") { }

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.GreaterThan(member, constant1);
        }
    }
}