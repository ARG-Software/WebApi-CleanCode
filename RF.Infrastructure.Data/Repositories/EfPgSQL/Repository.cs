using RF.Domain.Common;

namespace RF.Infrastructure.Data.Repositories.EfPgSQL
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using RF.Domain.Interfaces.Repositories.Generic;

    
    public class Repository<T> : BaseRepository<T>, IReadWriteRepository<T> where T : RFBaseEntity
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public void DeleteRFEntity(T entity)
        {
            Table.Remove(entity);
        }

        public void DeleteRFEntity(params T[] entities)
        {
            Table.RemoveRange(entities);
        }

        public void DeleteRFEntity(IEnumerable<T> listToDelete)
        {
            Table.RemoveRange(listToDelete);
        }

        public void DeleteRFEntityById(int id)
        {
            var entityToDelete = Table.FirstOrDefault(x => x.Id == id);
            if (entityToDelete != null)
            {
                Table.Remove(entityToDelete);
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return query.AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return await query.ToListAsync();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int index = 0, int size = 20, bool disableTracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return query.Skip(index * size).Take(size);
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int index = 0, int size = 20,
            bool disableTracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return await query.Skip(index * size).Take(size).ToListAsync();
        }

        public IEnumerable<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int index = 0, int size = 20, bool disableTracking = true, params Expression<Func<T, object>>[] includeProperties) where TResult : class
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return query.Select(selector).Skip(index * size).Take(size);
        }

        public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int index = 0, int size = 20, bool disableTracking = true, params Expression<Func<T, object>>[] includeProperties) where TResult : class
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return await query.Select(selector).Skip(index * size).Take(size).ToListAsync();
        }

        public void InsertRFEntity(T entity)
        {
            Table.Add(entity);
        }

        public void InsertRFEntity(params T[] entities)
        {
            Table.AddRange(entities);
        }

        public void InsertRFEntity(IEnumerable<T> entities)
        {
            Table.AddRange(entities);
        }

        public async Task InsertRFEntityAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task InsertRFEntityAsync(params T[] entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task InsertRFEntityAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public IEnumerable<T> Query(string sql, params object[] parameters)
        {
            var queryResult = MakeSqlQuery(sql, parameters);
            return queryResult.ToList();
        }

        protected virtual IQueryable<T> MakeSqlQuery(string sql, params object[] parameters)
        {
            var queryResult = Table.FromSql(sql, parameters);
            return queryResult;
        }

        public T Search(params object[] keyValues)
        {
            var searchResult = Table.Find(keyValues);
            return searchResult;
        }

        public async Task<T> SearchAsync(params object[] keyValues)
        {
            var searchResult = Table.FindAsync(keyValues);
            return await searchResult;
        }

        public int CountTableSize(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, null, true, includeProperties);

            return query.Count();
        }

        public async Task<int> CountTableSizeAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, null, true, includeProperties);

            return await query.CountAsync();
        }

        public T Single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);

            return query.FirstOrDefault();
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, orderBy, disableTracking, includeProperties);
            return await query.FirstOrDefaultAsync();
        }

        public void UpdateRFEntity(T entity)
        {
            Table.Update(entity);
        }

        public void UpdateRFEntity(params T[] entities)
        {
            Table.UpdateRange(entities);
        }

        public void UpdateRFEntity(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
        }
    }
}