using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "less than or equal" comparison.
    /// </summary>
    public class LessThanOrEqualTo : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public LessThanOrEqualTo() : base("LessThanOrEqualTo") { }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.LessThanOrEqual(member, constant1);
        }

        #endregion Public Methods
    }
}