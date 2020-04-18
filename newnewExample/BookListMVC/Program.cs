using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace BookListMVC
{
    public class Program
    {
        // initial this is a console application
        // after that static method defined below it becomes a web app
        public static void Main(string[] args)
        {
            CreateHostBuilder(args) // returns IHostBuilder
                .Build() // builds that host
                .Run(); // runs that host=now listen to requests
        }

        //
        public static IHostBuilder CreateHostBuilder(string[] args) =>  Host
            .CreateDefaultBuilder(args) // creates Host-builder object
            // with defaults
            .ConfigureWebHostDefaults(webBuilder => {webBuilder.UseStartup<Startup>();})
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
