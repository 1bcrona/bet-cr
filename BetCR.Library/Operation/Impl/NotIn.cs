using System;
using System.Collections;
using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing the inverse of a list "Contains" method call.
    /// </summary>
    public class NotIn : OperationBase
    {
        /// <inheritdoc />
        public NotIn() : base("NotIn") { }

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            if (!(constant1.Value is IList) || !constant1.Value.GetType().IsGenericType)
            {
                throw new ArgumentException("The 'NotIn' operation only supports lists as parameters.");
            }

            var type = constant1.Value.GetType();
            var inInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });
            var contains = Expression.Call(constant1, inInfo, member);
            return Expression.Not(contains);
        }
    }
}