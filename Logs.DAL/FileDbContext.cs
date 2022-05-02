using FileContextCore;
using Logs.BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logs.DAL
{
    public class FileDbContext : TablesSettings
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Default: JSON-Serializer
            optionsBuilder.UseFileContextDatabase();
            // Set the Database location
            optionsBuilder.UseFileContextDatabase(location: @"./db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the primary
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

    }
}
