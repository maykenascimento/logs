using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.Shared;

namespace Logs.BLL.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams);
        Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams, Expression<Func<T, bool>> predicate);
        Task<QueryResult<T>> GetPageAsync(QueryParameters queryParams, List<Expression<Func<T, object>>> includes);
        Task<QueryResult<T>> GetPageAsync<TProperty>(QueryParameters queryParams, Expression<Func<T, bool>> predicate, List<Expression<Func<T, TProperty>>> includes = null);
        Task<QueryResult<T>> GetOrderedPageQueryResultAsync(QueryParameters queryParams, IQueryable<T> query);
    }
}
