using Logs.BLL.Entities;
using Logs.BLL.Interfaces;

namespace Logs.DAL.Repositories.Api
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        public AuditRepository(AppDbContext context) : base(context, "log-types") { }
    }
}
