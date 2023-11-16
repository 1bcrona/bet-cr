using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an "less than" comparison.
    /// </summary>
    public class LessThan : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public LessThan() : base("LessThan")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.LessThan(member, constant1);
        }

        #endregion Public Methods
    }
}