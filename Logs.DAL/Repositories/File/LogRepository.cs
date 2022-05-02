using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Logs.BLL.Interfaces;
using Logs.Shared;

namespace Logs.DAL.Repositories.File
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(AppDbContext context) : base(context) { }

        public Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<Log>> GetPageAsync(QueryWithTypeParameters queryParams, Expression<Func<Log, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
