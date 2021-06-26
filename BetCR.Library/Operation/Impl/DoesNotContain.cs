using System.Linq.Expressions;
using System.Reflection;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation that checks for the non-existence of a substring within another string.
    /// </summary>
    public class DoesNotContain : OperationBase
    {
        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        /// <inheritdoc />
        public DoesNotContain() : base("DoesNotContain") { }
      
        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1.TrimToLower();

            return Expression.Not(Expression.Call(member.TrimToLower(), stringContainsMethod, constant))
                   .AddNullCheck(member);
        }
    }
}