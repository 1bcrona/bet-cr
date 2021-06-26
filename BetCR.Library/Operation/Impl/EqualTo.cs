using System.Linq.Expressions;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    public class EqualTo : OperationBase
    {
        public EqualTo() : base("EqualTo") { }
      
        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1;

            if (member.Type == typeof(string))
            {
                //constant = constant1.TrimToLower();

                return Expression.Equal(member, constant)
                       .AddNullCheck(member);
            }

            return Expression.Equal(member, constant);
        }
    }
}
