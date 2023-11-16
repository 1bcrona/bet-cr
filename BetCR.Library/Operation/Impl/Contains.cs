using BetCR.Library.Operation.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace BetCR.Library.Operation.Impl
{
    public class Contains : OperationBase
    {
        #region Private Fields

        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] {typeof(string)});

        #endregion Private Fields

        #region Public Constructors

        public Contains() : base("Contains")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), stringContainsMethod, constant)
                .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}