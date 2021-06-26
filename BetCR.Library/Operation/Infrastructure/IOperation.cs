using System.Linq.Expressions;

namespace BetCR.Library.Operation.Infrastructure
{
    public interface IOperation
    {
        string Name { get; }

        Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2);

    }
}
