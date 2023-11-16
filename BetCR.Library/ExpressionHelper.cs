using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BetCR.Library
{
    public static class ExpressionHelper
    {
        #region Public Methods

        public static Expression<Func<T, bool>> CreateExpression<T>(IEnumerable<Condition.Condition> query)
        {
            var groups = query.GroupBy(c => c.ColumnName);

            Expression<Func<T, bool>> exp = null;
            var param = Expression.Parameter(typeof(T), "X");

            foreach (var group in groups)
            {
                Expression<Func<T, bool>> groupExp = null;
                foreach (var condition in group)
                {
                    var columNames = condition.ColumnName.Split('.');
                    var con = GetExpression(param, condition, condition.ColumnName) as Expression<Func<T, bool>>;

                    if (groupExp == null)
                    {
                        groupExp = con;
                    }
                    else
                    {
                        var body = Expression.OrElse(groupExp.Body, con?.Body);
                        groupExp = Expression.Lambda<Func<T, bool>>(body, groupExp.Parameters[0]);
                    }
                }

                if (exp != null)
                {
                    var body = Expression.AndAlso(groupExp.Body, exp.Body);
                    exp = Expression.Lambda<Func<T, bool>>(body, groupExp.Parameters[0]);
                }
                else
                {
                    exp = groupExp;
                }
            }

            return exp;
        }

        public static Expression GetExpression(Expression parameter, Condition.Condition condition, string columName)
        {
            Expression expression;
            Type childType = null;
            var innerProperty = columName.IndexOf(".");
            var containsInnerProperty = innerProperty > -1;

            if (containsInnerProperty)
            {
                parameter = Expression.Property(parameter, columName.Substring(0, innerProperty));
                var isCollection = typeof(IEnumerable).IsAssignableFrom(parameter.Type);
                Expression parameterExpression;
                if (isCollection)
                {
                    childType = parameter.Type.GetGenericArguments()[0];
                    parameterExpression = Expression.Parameter(childType, childType.Name);
                }
                else
                {
                    parameterExpression = parameter;
                }

                var innerProperties = columName.Substring(innerProperty + 1);
                expression = GetExpression(parameterExpression, condition, innerProperties);
                if (isCollection)
                {
                    var anyMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == "Any" && m.GetParameters().Length == 2);
                    anyMethod = anyMethod.MakeGenericMethod(childType);
                    expression = Expression.Call(anyMethod, parameter, expression);
                    return MakeLambda(parameter, expression);
                }
                else
                {
                    return expression;
                }
            }
            else
            {
                var childProperty = parameter.Type.GetProperty(columName);
                var ex = Expression.Property(parameter, childProperty);
                expression = GetExpression(ex, condition);
                return MakeLambda(parameter, expression);
            }
        }

        public static Expression GetExpression(Expression ex, Condition.Condition condition)
        {
            var operation = Operation.Impl.Operation.ByName(condition.Type);
            if (operation == null) throw new Exception("OPERATION_NOT_FOUND");

            var con = operation.GetExpression(ex,
                Expression.Constant(condition.Values[0]),
                Expression.Constant(condition.Values.Length == 2 ? condition.Values[1] : null));
            return con;
        }

        public static Expression<Func<T, bool>> ToExpression<T>(this List<Condition.Condition> query)
        {
            if (query == null || query.Count == 0) return GetTrueExpression(typeof(T)) as Expression<Func<T, bool>>;
            return CreateExpression<T>(query);
        }

        #endregion Public Methods

        #region Private Methods

        private static Expression GetTrueExpression(Type type)
        {
            return Expression.Lambda(Expression.Constant(true), Expression.Parameter(type, "_"));
        }

        private static Expression MakeLambda(Expression parameter, Expression predicate)
        {
            var resultParameterVisitor = new ParameterVisitor();
            resultParameterVisitor.Visit(parameter);
            var resultParameter = resultParameterVisitor.Parameter;
            return Expression.Lambda(predicate, (ParameterExpression) resultParameter);
        }

        #endregion Private Methods

        #region Private Classes

        private class ParameterVisitor : ExpressionVisitor
        {
            #region Public Properties

            public Expression Parameter { get; private set; }

            #endregion Public Properties

            #region Protected Methods

            protected override Expression VisitParameter(ParameterExpression node)
            {
                Parameter = node;
                return node;
            }

            #endregion Protected Methods
        }

        #endregion Private Classes
    }
}