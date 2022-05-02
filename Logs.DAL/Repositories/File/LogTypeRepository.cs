using Logs.BLL.Entities;
using Logs.BLL.Interfaces;

namespace Logs.DAL.Repositories.File
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        public AuditRepository(AppDbContext context) : base(context) { }
    }
}
