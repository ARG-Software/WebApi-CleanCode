namespace RF.Domain.Interfaces.Repositories.Generic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    public interface IReadRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Queries the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Query(string sql, params object[] parameters);

        /// <summary>
        /// Searches the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        TEntity Search(params object[] keyValues);

        /// <summary>
        /// Searches the specified key values asynchronously.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        Task<TEntity> SearchAsync(params object[] keyValues);

        /// <summary>
        /// Returns a a single entry that matches the condition.
        /// </summary>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        TEntity Single(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Returns a a single entry that matches the condition asynchronously.
        /// </summary>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets all elements.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets all elements asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the list paged.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the list paged asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets the list paged and outputed to a different type(using selector). Example from Artist to ArtistDto
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IEnumerable<TResult> GetList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties
            ) where TResult : class;

        /// <summary>
        /// Gets the list paged and outputed to a different type(using selector) asynchronously. Example from Artist to ArtistDto
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="disableTracking">if set to <c>true</c> [disable tracking].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<IEnumerable<TResult>> GetListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties
        ) where TResult : class;

        /// <summary>
        /// Counts the size of the table.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        int CountTableSize(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Counts the size of the table asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        Task<int> CountTableSizeAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}