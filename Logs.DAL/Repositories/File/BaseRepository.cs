using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.BLL.Interfaces;
using Logs.Shared;

namespace Logs.DAL.Repositories.File
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public BaseRepository(AppDbContext context)
        {
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<T>> GetOrderedPageQueryResultAsync(QueryParameters queryObjectParams, IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<T>> GetPageAsync(QueryParameters queryObjectParams)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<T>> GetPageAsync(QueryParameters queryObjectParams, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<T>> GetPageAsync(QueryParameters queryObjectParams, List<Expression<Func<T, object>>> includes)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<T>> GetPageAsync<TProperty>(QueryParameters queryObjectParams, Expression<Func<T, bool>> predicate, List<Expression<Func<T, TProperty>>> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
