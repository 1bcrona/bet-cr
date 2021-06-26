using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a null check.
    /// </summary>
    public class IsNull : OperationBase
    {
        /// <inheritdoc />
        public IsNull() : base("IsNull") { }
    

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member, Expression.Constant(null));
        }
    }
}