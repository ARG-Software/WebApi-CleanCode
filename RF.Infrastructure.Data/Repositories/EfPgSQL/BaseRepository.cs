namespace RF.Infrastructure.Data.Repositories.EfPgSQL
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseRepository<T> where T : class
    {
        protected BaseRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentException(nameof(context));
            Table = Context.Set<T>();
        }

        public DbSet<T> Table { get; }

        public DbContext Context { get; }

        // Common methods
        protected IQueryable<T> BuildQuery(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, bool disableTracking, Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return disableTracking ? query.AsNoTracking() : query;
        }
    }
}