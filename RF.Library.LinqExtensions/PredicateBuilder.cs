using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RF.Library.LinqExtensions
{
    /// <summary>
    /// Enables the efficient, dynamic composition of query predicates.
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// Start an expression chain
        /// </summary>
        public static Expression<Func<T, bool>> New<T>(bool value = false)
        {
            if (value)
                return param => true; //value cannot be used in place of true/false

            return param => false;
        }

        /// <summary>
        /// Creates a predicate expression from the specified lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
        {
            return predicate;
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.AndAlso);
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "or".
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.OrElse);
        }

        /// <summary>
        /// Creates a predicate that evaluates to true.
        /// </summary>
        public static Expression<Func<T, bool>> True<T>()
        {
            return param => true;
        }

        /// <summary>
        /// Creates a predicate that evaluates to false.
        /// </summary>
        public static Expression<Func<T, bool>> False<T>()
        {
            return param => false;
        }

        /// <summary>
        /// Negates the predicate.
        /// </summary>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        #region private

        private static Expression<Func<T, bool>> CombineLambdas<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right, ExpressionType expressionType)
        {
            //Remove expressions created with Start<T>()
            if (IsExpressionBodyConstant(left))
                return (right);

            ParameterExpression p = left.Parameters[0];

            SubstituteParameterVisitor visitor = new SubstituteParameterVisitor
            {
                Sub = { [right.Parameters[0]] = p }
            };

            Expression body = Expression.MakeBinary(expressionType, left.Body, visitor.Visit(right.Body)!);
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        private static bool IsExpressionBodyConstant<T>(Expression<Func<T, bool>> left)
        {
            return left.Body.NodeType == ExpressionType.Constant;
        }

        internal class SubstituteParameterVisitor : ExpressionVisitor
        {
            public Dictionary<Expression, Expression> Sub = new Dictionary<Expression, Expression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (Sub.TryGetValue(node, out var newValue))
                {
                    return newValue;
                }

                return node;
            }
        }

        #endregion private
    }
}