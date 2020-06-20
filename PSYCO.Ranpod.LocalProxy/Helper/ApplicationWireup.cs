using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PSYCO.JsonDatastore;
using PSYCO.Ranpod.LocalProxy.Models;
using PSYCO.Ranpod.LocalProxy.Models.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace PSYCO.Ranpod.LocalProxy.Helper
{
    public static class ApplicationWirup
    {
        public static void WireupSerilog(bool isService)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.LogEvent.ExcludeAdditionalProperties = true;
            // we do want JSON data
            columnOptions.Store.Add(StandardColumn.LogEvent);
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            if (!isService)
            {
                pathToContentRoot = "";
            }
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(pathToContentRoot, "fullLog.txt"))
                //.WriteTo.MSSqlServer(
                //    connectionString: config.GetConnectionString("DefaultConnection"),
                //    autoCreateSqlTable: true,
                //    tableName: "Logs", period: TimeSpan.FromSeconds(5),
                //    columnOptions: columnOptions
                //)
                .CreateLogger();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext">Type of application dbcontext</typeparam>
        /// <param name="app"></param>
        public static void EnsureLastMigrationApplyed<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
        {
            //ensure last migration applyed.
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
                context.Database.Migrate();
            }

        }


        public static T GetService<T>(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var serviceInstance = serviceScope.ServiceProvider.GetRequiredService<T>();
                return serviceInstance;

            }
        }
        public static void ScheduleSessionChecker(this IApplicationBuilder app)
        {
            var db = app.GetService<IJsonDataStore<JsonDatabase>>();
            ClientSessions.Initiate(db);

            var settings = app.GetService<IOptionsSnapshot<AppSettings>>().Value;
            var timer = new Timer(CheckSessionsAlive.DoJob, app, TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(settings.CheckSessionsIntervalSeconds));




        }







    }

}
