using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Logs.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Database connection
            string sourceDbType = Configuration.GetValue<string>("DatabaseType") ?? "sqlite";
            string dbFilePath = Path.Combine(Environment.CurrentDirectory, "db");
            string connectionString = Configuration.GetConnectionString("SqliteConnection");
            connectionString = connectionString.Replace("{appDir}", dbFilePath);

            // Create the DB folder if it not exists
            if (!Directory.Exists(dbFilePath))
            {
                Directory.CreateDirectory(dbFilePath);
            }

            // Set the type and connection with the Database
            Logs.DAL.Setup.AddDbContext(services, connectionString, sourceDbType);
            #endregion

            #region Repositories
            // Here we can switch between DAL repositories, this is set using appsettings.json
            switch (sourceDbType)
            {
                case "api":
                    services.AddTransient(typeof(IBaseRepository<>), typeof(Logs.DAL.Repositories.Api.BaseRepository<>));
                    services.AddTransient<ILogRepository, Logs.DAL.Repositories.Api.LogRepository>();
                    services.AddTransient<ILogTypeRepository, Logs.DAL.Repositories.Api.LogTypeRepository>();
                    services.AddTransient<IAuditRepository, Logs.DAL.Repositories.Api.AuditRepository>();
                    break;
                case "file":
                    //
                    // Using the FileContextCore plugin to get all the functionality to use file as a database instead of building all the logic
                    //
                    //services.AddTransient(typeof(IBaseRepository<>), typeof(Logs.DAL.Repositories.File.BaseRepository<>));
                    //services.AddTransient<ILogRepository, Logs.DAL.Repositories.File.LogRepository>();
                    //services.AddTransient<ILogTypeRepository, Logs.DAL.Repositories.File.LogTypeRepository>();
                    //services.AddTransient<IAuditRepository, Logs.DAL.Repositories.File.AuditRepository>();
                    //break;
                default: // sqlite
                    services.AddTransient(typeof(IBaseRepository<>), typeof(Logs.DAL.Repositories.Database.BaseRepository<>));
                    services.AddTransient<ILogRepository, Logs.DAL.Repositories.Database.LogRepository>();
                    services.AddTransient<ILogTypeRepository, Logs.DAL.Repositories.Database.LogTypeRepository>();
                    services.AddTransient<IAuditRepository, Logs.DAL.Repositories.Database.AuditRepository>();
                    break;
            }
            #endregion

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Logs}/{action=Index}/{id?}");
            });
        }
    }
}
