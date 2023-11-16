using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a check for a non-empty string.
    /// </summary>
    public class IsNotEmpty : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public IsNotEmpty() : base("IsNotEmpty")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.NotEqual(member.TrimToLower(), Expression.Constant(string.Empty))
                .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}