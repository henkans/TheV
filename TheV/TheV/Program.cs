using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
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

                .AddScoped<IProcessManager, ProcessManager>() //Singleton
                .AddScoped<IVersionCheckerManager, VersionCheckerManager>() // Note Do all work here?
                .AddScoped<IOutputConsoleManager, OutputConsoleManager>()

                //add all version handlers
                .AddScoped<IVersionChecker, OsVersionChecker>()
                .AddScoped<IVersionChecker, NetCoreRuntimeVersionChecker>()
                .AddScoped<IVersionChecker, NetCoreSdkVersionChecker>()
                .AddScoped<IVersionChecker, NetVersionChecker>()
                .AddScoped<IVersionChecker, NodeVersionChecker>()
                //.AddScoped<IVersionChecker, NpmVersionChecker>() // Note: Not working. Why? It's in path...
                .AddScoped<IVersionChecker, PsVersionChecker>()

                .BuildServiceProvider();


            var logger = serviceProvider.GetService<ILogger<Program>>();
            //logger.LogDebug("Start!");


            //Do the actual work here
            var outputConsoleManager = serviceProvider.GetRequiredService<IOutputConsoleManager>();
            var services = serviceProvider.GetServices<IVersionChecker>();
            foreach (var service in services)
            {
                //var checker = scope.ServiceProvider.GetRequiredService<IVersionChecker>();
                outputConsoleManager.Write(service);

            }


            //  using (var scope = serviceProvider.CreateScope())
            //  {
            //      logger.LogDebug("In scope!");
            //      var psVersionChecker = scope.ServiceProvider.GetRequiredService<IPsVersionChecker>();
            //      Console.WriteLine(psVersionChecker.GetVersion());
            //  }


            //logger.LogCritical("All done!");
            //logger.LogError("All done!");
            //logger.LogInformation("All done!");
            //logger.LogWarning("All done!");

            logger.LogDebug("All done!");

            //Console.WriteLine("Hello TheV!");
        }
    }
}
