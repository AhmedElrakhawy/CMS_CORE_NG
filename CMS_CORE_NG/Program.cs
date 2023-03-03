using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.Diagnostics;
using LoggingService;
using System.IO;
using Serilog.Sinks.SystemConsole.Themes;

namespace CMS_CORE_NG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((hostingContext, LoggingConfiguration) => LoggingConfiguration.Enrich.FromLogContext()
                      .Enrich.WithProperty("Application", "CMS_CORE_NG")
                      .Enrich.WithProperty("MachineName", Environment.MachineName)
                      .Enrich.WithProperty("CurrentManagedThreadId", Environment.CurrentManagedThreadId)
                      .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                      .Enrich.WithProperty("Version", Environment.Version)
                      .Enrich.WithProperty("UserName", Environment.UserName)
                      .Enrich.WithProperty("ProcessId", Process.GetCurrentProcess().Id)
                      .Enrich.WithProperty("ProcessName", Process.GetCurrentProcess().ProcessName)
                      .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                      .WriteTo.File(formatter: new CustomTextFormatter(), path: Path.Combine(hostingContext.HostingEnvironment.ContentRootPath + $"{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}", $"cms_core_ng_{DateTime.Now:yyyyMMdd}.txt"))
                     .ReadFrom.Configuration(hostingContext.Configuration));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
