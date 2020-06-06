using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Hahn.ApplicatonProcess.May2020.Web
{
    /// <summary>
    /// Application entry point class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Creates the web host
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.SetBasePath(System.IO.Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json");
                    });

                    // Removing the server header for security reasons
                    webBuilder.UseKestrel(o => o.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
