namespace RF.Domain.Interfaces.Filters
{
    using System;
    using System.Linq.Expressions;

    public interface ICreteria<T> where T : class
    {
        Expression<Func<T, bool>> ExpressionCriteria { get; set; }
    }
}