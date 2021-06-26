using System.Linq.Expressions;
using System.Reflection;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    public class StartsWith : OperationBase
    {
        private readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        public StartsWith() : base("StartsWith") { }
       

        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), startsWithMethod, constant)
                   .AddNullCheck(member);
        }
    }
}
