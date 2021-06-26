using System.Linq.Expressions;
using BetCR.Library.Operation.Infrastructure;

namespace BetCR.Library.Operation.Base
{

    public abstract class OperationBase : IOperation
    {
        public string Name { get; }

        public abstract System.Linq.Expressions.Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2);

        protected OperationBase(string name)
        {
            Name = name;

        }
    }
}
