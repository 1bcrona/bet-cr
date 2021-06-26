using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing an inequality comparison.
    /// </summary>
    public class NotEqualTo : OperationBase
    {
        #region Public Constructors

        /// <inheritdoc />
        public NotEqualTo() : base("NotEqualTo") { }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1;

            if (member.Type == typeof(string))
            {
                constant = constant1.TrimToLower();

                return Expression.NotEqual(member.TrimToLower(), constant)
                       .AddNullCheck(member);
            }

            return Expression.NotEqual(member, constant);
        }

        #endregion Public Methods
    }
}