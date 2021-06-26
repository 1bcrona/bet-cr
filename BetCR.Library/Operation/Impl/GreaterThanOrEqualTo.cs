using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "greater than or equal" comparison.
    /// </summary>
    public class GreaterThanOrEqualTo : OperationBase
    {
        /// <inheritdoc />
        public GreaterThanOrEqualTo() : base("GreaterThanOrEqualTo") { }


        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.GreaterThanOrEqual(member, constant1);
        }
    }
}