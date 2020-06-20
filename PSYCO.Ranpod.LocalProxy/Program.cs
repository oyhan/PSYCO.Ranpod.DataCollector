using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PSYCO.Ranpod.LocalProxy.Helper;
using Serilog;

namespace PSYCO.Ranpod.LocalProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            ApplicationWirup.WireupSerilog(isService);
          
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
             .Build();

            var port = config.GetSection("port").Value;
            var builder = CreateWebHostBuilder(port);


            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                builder.UseContentRoot(pathToContentRoot);
            }

            var host = builder.Build();

            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string port)
        {




            return WebHost.CreateDefaultBuilder()
                      .UseStartup<Startup>()
                          .UseUrls($"http://*:{port ?? "13173"}").UseSerilog();
        }

    }
}
