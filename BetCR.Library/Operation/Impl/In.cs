﻿using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using BetCR.Library.Operation.Base;

namespace BetCR.Library.Operation.Impl
{
    /// <summary>
    /// Operation representing a list "Contains" method call.
    /// </summary>
    public class In : OperationBase
    {
        public In() : base("In") { }


        public override Expression GetExpression(Expression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            if (!(constant1.Value is IList) || !constant1.Value.GetType().IsGenericType)
            {
                throw new ArgumentException("The 'In' operation only supports lists as parameters.");
            }




            var type = constant1.Value.GetType();
            var inInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });

            return GetExpressionHandlingNullables(member, constant1, type, inInfo) ?? Expression.Call(constant1, inInfo, member);
        }

        private Expression GetExpressionHandlingNullables(Expression member, ConstantExpression constant1, Type type,
            MethodInfo inInfo)
        {
            var listUnderlyingType = Nullable.GetUnderlyingType(type.GetGenericArguments()[0]);
            var memberUnderlingType = Nullable.GetUnderlyingType(member.Type);
            if (listUnderlyingType != null && memberUnderlingType == null)
            {
                return Expression.Call(constant1, inInfo, member);
            }

            return null;
        }
    }
}