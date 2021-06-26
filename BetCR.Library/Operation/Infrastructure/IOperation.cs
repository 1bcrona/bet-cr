using System.Linq.Expressions;

namespace BetCR.Library.Operation.Infrastructure
{
    public interface IOperation
    {
        #region Public Properties

        string Name { get; }

        #endregion Public Properties

        #region Public Methods

        Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2);

        #endregion Public Methods
    }
}