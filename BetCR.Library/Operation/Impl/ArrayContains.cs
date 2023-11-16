using BetCR.Library.Operation.Base;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace BetCR.Library.Operation.Impl
{
    public class ArrayContains : OperationBase
    {
        #region Public Constructors

        public ArrayContains() : base("ArrayContains")
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var isCollection = typeof(IEnumerable).IsAssignableFrom(member.Type);
            if (!isCollection) throw new ArgumentException("The 'IArrayContainsn' operation only supports List Properties.");

            var arrayContainsMethod = member.Type.GetMethod("Contains", new[] {member.Type.GetGenericArguments()[0]});

            return Expression.Call(member, arrayContainsMethod, constant1)
                .AddNullCheck(member);
        }

        #endregion Public Methods
    }
}