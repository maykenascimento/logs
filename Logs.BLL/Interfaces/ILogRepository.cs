using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.Shared;

namespace Logs.BLL.Interfaces
{
    public interface ILogRepository : IBaseRepository<Log>
    {
        Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams);
        Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams, Expression<Func<Log, bool>> predicate);
    }
}
