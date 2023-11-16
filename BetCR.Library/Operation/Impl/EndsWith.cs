using BetCR.Library.Operation.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace BetCR.Library.Operation.Impl
{
    public class EndsWith : OperationBase
    {
        #region Private Fields

        private readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] {typeof(string)});

        #endregion Private Fields

        #region Public Constructors

        public EndsWith() : base("EndsWith")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), endsWithMethod, constant)
                .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}