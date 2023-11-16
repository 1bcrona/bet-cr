using BetCR.Library.Operation.Base;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    public class EqualTo : OperationBase
    {
        #region Public Constructors

        public EqualTo() : base("EqualTo")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1;

            if (member.Type == typeof(string))
                //constant = constant1.TrimToLower();
                return Expression.Equal(member, constant)
                    .AddNullCheck(member);

            return Expression.Equal(member, constant);
        }

        #endregion Public Methods
    }
}