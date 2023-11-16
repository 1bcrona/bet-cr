using BetCR.Library.Operation.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace BetCR.Library.Operation.Impl
{
    public class StartsWith : OperationBase
    {
        #region Private Fields

        private readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] {typeof(string)});

        #endregion Private Fields

        #region Public Constructors

        public StartsWith() : base("StartsWith")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), startsWithMethod, constant)
                .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}