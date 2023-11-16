using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a "not-null" check.
    /// </summary>
    public class IsNotNull : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public IsNotNull() : base("IsNotNull")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.NotEqual(member, Expression.Constant(null));
        }

        #endregion Public Methods
    }
}