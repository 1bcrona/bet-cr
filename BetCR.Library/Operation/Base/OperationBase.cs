using BetCR.Library.Operation.Infrastructure;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Base
{
    public abstract class OperationBase : IOperation
    {
        #region Protected Constructors

        protected OperationBase(string name)
        {
            Name = name;
        }

        #endregion Protected Constructors

        #region Public Properties

        public string Name { get; }

        #endregion Public Properties

        #region Public Methods

        public abstract Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2);

        #endregion Public Methods
    }
}