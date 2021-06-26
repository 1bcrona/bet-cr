using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a range comparison.
    /// </summary>
    public class Between : OperationBase
    {
        /// <inheritdoc />
        public Between() : base("Between") { }


        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var left = Expression.GreaterThanOrEqual(member, constant1);
            var right = Expression.LessThanOrEqual(member, constant2);

            return Expression.AndAlso(left, right);
        }
    }
}