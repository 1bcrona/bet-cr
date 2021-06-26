using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a "null or whitespace" check.
    /// </summary>
    public class IsNullOrWhiteSpace : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public IsNullOrWhiteSpace() : base("IsNullOrWhiteSpace") { }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression exprNull = Expression.Constant(null);
            Expression exprEmpty = Expression.Constant(string.Empty);
            return Expression.OrElse(
                Expression.Equal(member, exprNull),
                Expression.AndAlso(
                    Expression.NotEqual(member, exprNull),
                    Expression.Equal(member.TrimToLower(), exprEmpty)));
        }

        #endregion Public Methods
    }
}