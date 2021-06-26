using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a "not null nor whitespace" check.
    /// </summary>
    public class IsNotNullNorWhiteSpace : OperationBase
    {
        /// <inheritdoc />
        public IsNotNullNorWhiteSpace() : base("IsNotNullNorWhiteSpace") { }


        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression exprNull = Expression.Constant(null);
            Expression exprEmpty = Expression.Constant(string.Empty);
            return Expression.AndAlso(
                Expression.NotEqual(member, exprNull),
                Expression.NotEqual(member.TrimToLower(), exprEmpty));
        }
    }
}