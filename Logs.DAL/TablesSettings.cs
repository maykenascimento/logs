using System;
using Logs.BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logs.DAL
{
    public class TablesSettings : DbContext
    {
        public TablesSettings() { }
        public TablesSettings(DbContextOptions<AppDbContext> options) : base(options) { }

        #region Tables declarations
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
        public DbSet<Audit> Audits { get; set; }
        #endregion
    }
}
