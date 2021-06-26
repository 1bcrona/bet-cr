
using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a "not-null" check.
    /// </summary>
    public class IsNotNull : OperationBase
    {
        /// <inheritdoc />
        public IsNotNull() : base("IsNotNull") { }
   

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.NotEqual(member, Expression.Constant(null));
        }
    }
}