using System.Linq.Expressions;
using System.Reflection;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    public class EndsWith : OperationBase
    {
        private readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public EndsWith() : base("EndsWith") { }
       

        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), endsWithMethod, constant)
                   .AddNullCheck(member);
        }
    }
}
