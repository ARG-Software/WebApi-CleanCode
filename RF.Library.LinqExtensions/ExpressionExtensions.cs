namespace RF.Library.LinqExtensions
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> CastPredicateExpressionFromTo<T1, T>(this Expression<Func<T1, bool>> predicate) where T : class
        {
            var castedExpression = Expression.Lambda<Func<T, bool>>(Expression.Convert(predicate.Body, typeof(T)));
            return castedExpression;
        }

        public static Expression<Func<T, object>> CreatePropSelectorExpression<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.Convert(Expression.PropertyOrField(parameter, propertyName), typeof(object));
            return Expression.Lambda<Func<T, object>>(body, parameter);
        }
    }
}