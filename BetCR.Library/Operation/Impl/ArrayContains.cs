using System;
using System.Collections;
using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    public class ArrayContains : OperationBase
    {

        public ArrayContains() : base("ArrayContains") { }



        public override System.Linq.Expressions.Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var isCollection = typeof(IEnumerable).IsAssignableFrom(member.Type);
            if (!isCollection)
            {
                throw new ArgumentException("The 'IArrayContainsn' operation only supports List Properties.");
            }

            var arrayContainsMethod = member.Type.GetMethod("Contains", new[] { member.Type.GetGenericArguments()[0] });


            return System.Linq.Expressions.Expression.Call(member, arrayContainsMethod, constant1)
                   .AddNullCheck(member);
        }
    }
}
