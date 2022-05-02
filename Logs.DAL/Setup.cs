using FileContextCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logs.DAL
{
    public static class Setup
    {
        /// <summary>
        /// This function is responsible for defining the Database instance passing its connection string.
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <param name="connectionString">String with the Database connection settings</param>
        /// <param name="dataSourceType">The type of database source: sqlite; file; api</param>
        public static void AddDbContext(this IServiceCollection services, string connectionString, string dataSourceType = "sqlite")
        {
            switch (dataSourceType)
            {
                case "file":
                    services.AddDbContext<AppDbContext>(options => options.UseFileContextDatabase());
                    break;
                case "api":
                default:
                    services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
                    break;
            }

        }
    }
}
