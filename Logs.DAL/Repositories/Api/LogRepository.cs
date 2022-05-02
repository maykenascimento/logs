using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.BLL.Interfaces;
using Logs.Shared;

namespace Logs.DAL.Repositories.Api
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(AppDbContext context) : base(context, "logs") { }

        public async Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, null);
        }

        public async Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams, Expression<Func<Log, bool>> predicate)
        {
            return await GetOrderedPageQueryResultAsync(queryParams, null);
        }
    }
}
