using Logs.BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logs.DAL
{
    public  class DataInitializer
    {
        // Insert the initial data into the Database
        public static void InitializeData(ModelBuilder modelBuilder)
        {
            #region Logs type
            modelBuilder.Entity<LogType>()
                .HasData(
                    new LogType { Id = 1, Name = "Information" },
                    new LogType { Id = 2, Name = "Warning" },
                    new LogType { Id = 3, Name = "Error" }
                );
            #endregion
        }
    }
}
