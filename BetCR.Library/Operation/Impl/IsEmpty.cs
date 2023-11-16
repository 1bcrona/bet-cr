using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a check for an empty string.
    /// </summary>
    public class IsEmpty : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public IsEmpty() : base("IsEmpty")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member.TrimToLower(), Expression.Constant(string.Empty))
                .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}