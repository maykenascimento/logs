using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.BLL.Interfaces;
using Logs.Shared;

namespace Logs.DAL.Repositories.Database
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(AppDbContext context) : base(context) { }

        public async Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams)
        {
            IQueryable<Log> query = _context.Logs;

            if (queryParams.LogType > 0)
            {
                query = query.Where(s => s.LogTypeId == queryParams.LogType.GetHashCode());
            }

            if (!string.IsNullOrEmpty(queryParams.Keywords))
            {
                query = query.Where(q => q.Description.Contains(queryParams.Keywords));
            }

            return await GetOrderedPageQueryResultAsync(queryParams, query);
        }

        public async Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams, Expression<Func<Log, bool>> predicate)
        {
            IQueryable<Log> query = _context.Logs;

            if (queryParams.LogType > 0)
            {
                query = query.Where(s => s.LogTypeId == queryParams.LogType.GetHashCode());
            }

            if (predicate != null) query = query.Where(predicate);

            return await GetOrderedPageQueryResultAsync(queryParams, query);
        }
    }
}
