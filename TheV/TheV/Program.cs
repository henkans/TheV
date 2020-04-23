using System;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TheV.Checkers;
using TheV.Checkers.Interfaces;
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
                .AddScoped<IOsVersionChecker, OsVersionChecker>()
                .AddScoped<INpmVersionChecker, NpmVersionChecker>()
                .AddScoped<INetCoreRuntimeVersionChecker, NetCoreRuntimeVersionChecker>()
                .AddScoped<INetCoreSdkVersionChecker, NetCoreSdkVersionChecker>()
                .AddScoped<INetVersionChecker, NetVersionChecker>()
                .AddScoped<INodeVersionChecker, NodeVersionChecker>()


                //Singleton
                .AddScoped<IProcessManager, ProcessManager>()

                .BuildServiceProvider();


            var logger = serviceProvider.GetService<ILogger<Program>>();
            //logger.LogDebug("All done!");

            //do the actual work here
            //var bar = serviceProvider.GetService<IBarService>();
            //bar.DoSomeRealWork();

            using (var scope = serviceProvider.CreateScope())
            {
                logger.LogDebug("In scope!");

                var osVersionChecker = scope.ServiceProvider.GetRequiredService<IOsVersionChecker>();
                Console.WriteLine(osVersionChecker.GetVersion());

                var netVersionChecker = scope.ServiceProvider.GetRequiredService<INetVersionChecker>();
                Console.WriteLine(netVersionChecker.GetVersion());


                var netCoreSdkVersionChecker = scope.ServiceProvider.GetRequiredService<INetCoreSdkVersionChecker>();
                Console.WriteLine(netCoreSdkVersionChecker.GetVersion());

                var netCoreVersionChecker = scope.ServiceProvider.GetRequiredService<INetCoreRuntimeVersionChecker>();
                Console.WriteLine(netCoreVersionChecker.GetVersion());

                var nodeVersionChecker = scope.ServiceProvider.GetRequiredService<INodeVersionChecker>();
                Console.WriteLine(nodeVersionChecker.GetVersion());

                //var npmVersionHandler = scope.ServiceProvider.GetRequiredService<INpmVersionHandler>();
                //Console.WriteLine(npmVersionHandler.GetVersion());

            }


            //logger.LogCritical("All done!");
            //logger.LogError("All done!");
            //logger.LogInformation("All done!");
            //logger.LogWarning("All done!");

            logger.LogDebug("All done!");

            //Console.WriteLine("Hello TheV!");
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
