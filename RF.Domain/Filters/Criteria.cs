namespace RF.Domain.Filters
{
    using System;
    using System.Linq.Expressions;
    using RF.Domain.Interfaces.Filters;

    public class Criteria<T> : ICreteria<T> where T : class
    {
        public Expression<Func<T, bool>> ExpressionCriteria { get; set; }
    }
}