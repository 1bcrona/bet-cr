using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a check for an empty string.
    /// </summary>
    public class IsEmpty : OperationBase
    {
        /// <inheritdoc />
        public IsEmpty() : base("IsEmpty") { }
   

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member.TrimToLower(), Expression.Constant(string.Empty))
                   .AddNullCheck(member);
        }
    }
}