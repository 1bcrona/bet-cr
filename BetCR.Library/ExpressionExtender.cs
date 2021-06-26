using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BetCR.Library
{
    public static class ExpressionExtender
    {
        #region Private Fields

        private static readonly MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);
        private static readonly MethodInfo trimMethod = typeof(string).GetMethod("Trim", new Type[0]);

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Adds a "null check" to the expression (before the original one).
        /// </summary>
        /// <param name="expression">Expression to which the null check will be pre-pended.</param>
        /// <param name="member">Member that will be checked.</param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression AddNullCheck(this System.Linq.Expressions.Expression expression, Expression member)
        {
            System.Linq.Expressions.Expression memberIsNotNull = System.Linq.Expressions.Expression.NotEqual(member, System.Linq.Expressions.Expression.Constant(null));
            return System.Linq.Expressions.Expression.AndAlso(memberIsNotNull, expression);
        }

        public static System.Linq.Expressions.Expression TrimToLower(this Expression member)
        {
            var trimMemberCall = System.Linq.Expressions.Expression.Call(member, trimMethod);
            return System.Linq.Expressions.Expression.Call(trimMemberCall, toLowerMethod);
        }

        /// <summary>
        /// Applies the string Trim and ToLower methods to an ExpressionMember.
        /// </summary>
        /// <param name="constant">Constant to which to methods will be applied.</param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression TrimToLower(this ConstantExpression constant)
        {
            var trimMemberCall = System.Linq.Expressions.Expression.Call(constant, trimMethod);
            return System.Linq.Expressions.Expression.Call(trimMemberCall, toLowerMethod);
        }

        #endregion Public Methods
    }
}