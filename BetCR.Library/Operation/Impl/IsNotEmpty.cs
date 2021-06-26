using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a check for a non-empty string.
    /// </summary>
    public class IsNotEmpty : OperationBase
    {
        /// <inheritdoc />
        public IsNotEmpty() : base("IsNotEmpty") { }


        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.NotEqual(member.TrimToLower(), Expression.Constant(string.Empty))
                   .AddNullCheck(member);
        }
    }
}