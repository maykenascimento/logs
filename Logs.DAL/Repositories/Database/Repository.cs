using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.BLL.Interfaces;
using Logs.DAL.Extensions;
using Logs.Shared;
using Microsoft.EntityFrameworkCore;

namespace Logs.DAL.Repositories.Database
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> table = null;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = table.AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<System.Collections.Generic.List<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public virtual async Task<System.Collections.Generic.List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = table.AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var expression in includes)
                {
                    query = query.Include(expression);
                }
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        #region Pagination
        public virtual async Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, table);
        }

        public virtual async Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams, Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = table;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await GetOrderedPageQueryResultAsync(queryParams, query);
        }

        public virtual async Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams, System.Collections.Generic.List<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = table;

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await GetOrderedPageQueryResultAsync(queryParams, query);
        }

        public virtual async Task<QueryResult<T>> GetPageAsync<TProperty>(QueryParameters queryParams, Expression<Func<T, bool>> predicate, System.Collections.Generic.List<Expression<Func<T, TProperty>>> includes = null)
        {
            IQueryable<T> query = table;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await GetOrderedPageQueryResultAsync(queryParams, query);
        }

        public virtual async Task<QueryResult<T>> GetOrderedPageQueryResultAsync(QueryParameters queryParams, IQueryable<T> query)
        {
            IQueryable<T> OrderedQuery = query;

            if (queryParams.SortingParams != null && queryParams.SortingParams.Count > 0)
            {
                OrderedQuery = SortingExtension.GetOrdering(query, queryParams.SortingParams);
            }

            var totalCount = await query.CountAsync();

            if (OrderedQuery != null)
            {
                var fecthedItems = await GetPagePrivateQuery(OrderedQuery, queryParams).ToListAsync();

                return new QueryResult<T>(fecthedItems, totalCount);
            }

            return new QueryResult<T>(await GetPagePrivateQuery(table, queryParams).ToListAsync(), totalCount);
        }

        private IQueryable<T> GetPagePrivateQuery(IQueryable<T> query, QueryParameters queryParams)
        {
            return query
                .Skip((((int)queryParams.PageIndex) - 1) * ((int)queryParams.PageSize))
                .Take((int)queryParams.PageSize);
        }
        #endregion
    }
}
