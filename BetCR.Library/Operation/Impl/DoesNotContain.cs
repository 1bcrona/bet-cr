using BetCR.Library.Operation.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation that checks for the non-existence of a substring within another string.
    /// </summary>
    public class DoesNotContain : OperationBase
    {
        #region Private Fields

        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        #endregion Private Fields

        #region Public Constructors

        /// <inheritdoc />
        public DoesNotContain() : base("DoesNotContain") { }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1.TrimToLower();

            return Expression.Not(Expression.Call(member.TrimToLower(), stringContainsMethod, constant))
                   .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}