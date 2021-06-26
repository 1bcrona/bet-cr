using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "less than or equal" comparison.
    /// </summary>
    public class LessThanOrEqualTo : OperationBase
    {
        /// <inheritdoc />
        public LessThanOrEqualTo() : base("LessThanOrEqualTo") { }

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.LessThanOrEqual(member, constant1);
        }
    }
}