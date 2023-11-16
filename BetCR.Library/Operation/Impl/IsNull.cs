using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a null check.
    /// </summary>
    public class IsNull : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public IsNull() : base("IsNull")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member, Expression.Constant(null));
        }

        #endregion Public Methods
    }
}