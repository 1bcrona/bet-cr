using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "greater than or equal" comparison.
    /// </summary>
    public class GreaterThanOrEqualTo : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public GreaterThanOrEqualTo() : base("GreaterThanOrEqualTo") { }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.GreaterThanOrEqual(member, constant1);
        }

        #endregion Public Methods
    }
}