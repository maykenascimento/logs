using System.Collections.Generic;
using System.Threading.Tasks;
using Logs.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Logs.DAL
{
    public class AppDbContext : TablesSettings
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogType>()
                .HasKey(k => k.Id);

            // Set the primary and create the relationship (One to many)
            modelBuilder.Entity<Log>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Log>()
                .HasOne(a => a.LogType)
                .WithMany()
                .HasForeignKey(f => f.LogTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Set the primary key
            modelBuilder.Entity<Audit>()
                .HasKey(k => k.Id);

            // Make sure the initial data is present in the DB
            DataInitializer.InitializeData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            //RecordAudit();
            var result = await base.SaveChangesAsync();
            return result;
        }

        // This function is used to track all tables changes
        private void RecordAudit()
        {
            ChangeTracker.DetectChanges();

            var audits = new List<Audit>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;

                var newEntry = new Audit();
                newEntry.CreatedAt = DateTime.UtcNow;
                newEntry.Description = @"";
                newEntry.TableName = entry.Entity.GetType().Name;
                newEntry.PrimaryKey = 0;

                switch (entry.State)
                {
                    case EntityState.Added:
                        newEntry.AuditType = BLL.Enums.AuditType.Create;
                        break;
                    case EntityState.Deleted:
                        newEntry.AuditType = BLL.Enums.AuditType.Delete;
                        break;
                    case EntityState.Modified:
                    default:
                        newEntry.AuditType = BLL.Enums.AuditType.Update;
                        break;
                }
            }

            foreach (var audit in audits)
            {
                Audits.Add(audit);
            }
        }
    }
}
