using Logs.BLL.Entities;
using Logs.BLL.Interfaces;

namespace Logs.DAL.Repositories.Api
{
    public class LogTypeRepository : BaseRepository<LogType>, ILogTypeRepository
    {
        public LogTypeRepository(AppDbContext context) : base(context, "audit") { }
    }
}
