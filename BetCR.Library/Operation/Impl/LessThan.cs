
using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "less than" comparison.
    /// </summary>
    public class LessThan : OperationBase
    {
        /// <inheritdoc />
        public LessThan() : base("LessThan") { }
     

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.LessThan(member, constant1);
        }
    }
}