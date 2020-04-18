using System;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TheV.Handlers;
using TheV.Managers;

namespace TheV
{
    class Program
    {

        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(logging =>
                {
                    //logging.AddConfiguration();
                    logging.AddConsole();

                })
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug)
                //All version handlers
                .AddScoped<IOsVersionHandler, OsVersionHandler>()
                .AddScoped<INpmVersionHandler, NpmVersionHandler>()
                .AddScoped<INetCoreVersionHandler, NetCoreVersionHandler>()
                .AddScoped<INodeVersionHandler, NodeVersionHandler>()



                .AddScoped<IProcessManager, ProcessManager>()

                .BuildServiceProvider();


            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogDebug("All done!");

            //do the actual work here
            //var bar = serviceProvider.GetService<IBarService>();
            //bar.DoSomeRealWork();

            using (var scope = serviceProvider.CreateScope())
            {
                var osHandler = scope.ServiceProvider.GetRequiredService<IOsVersionHandler>();
                var osInfo = osHandler.GetOsInfo();
                Console.WriteLine(osInfo.Print());
                logger.LogDebug("In scope!");


                var netCoreVersionHandler = scope.ServiceProvider.GetRequiredService<INetCoreVersionHandler>();
                Console.WriteLine(netCoreVersionHandler.GetVersion());

                var nodeVersionHandler = scope.ServiceProvider.GetRequiredService<INodeVersionHandler>();
                Console.WriteLine(nodeVersionHandler.GetVersion());

                //var npmVersionHandler = scope.ServiceProvider.GetRequiredService<INpmVersionHandler>();
                //Console.WriteLine(npmVersionHandler.GetVersion());

            }


            //logger.LogCritical("All done!");
            //logger.LogError("All done!");
            //logger.LogInformation("All done!");
            //logger.LogWarning("All done!");

            logger.LogDebug("All done!");

            Console.WriteLine("Hello TheV!");
        }


        //public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>()
        //    .ConfigureLogging(logging =>
        //    {
        //        // clear default logging providers
        //        logging.ClearProviders();

        //        // add built-in providers manually, as needed 
        //        logging.AddConsole();
        //        logging.AddDebug();
        //        logging.AddEventLog();
        //        logging.AddEventSourceLogger();
        //        logging.AddTraceSource(sourceSwitchName);
        //    });



    }
}
