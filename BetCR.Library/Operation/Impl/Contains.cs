using System.Linq.Expressions;
using System.Reflection;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    public class Contains : OperationBase
    {
        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        public Contains():base("Contains") {}
       


        public override System.Linq.Expressions.Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression constant = constant1.TrimToLower();

            return System.Linq.Expressions.Expression.Call(member.TrimToLower(), stringContainsMethod, constant)
                   .AddNullCheck(member);
        }
    }
}
