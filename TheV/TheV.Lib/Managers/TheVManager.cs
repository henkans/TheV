using System;
using System.Collections.Generic;
using System.Text;
using TheV.Lib.Checkers;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Models;

namespace TheV.Lib.Managers
{
    public class TheVManager
    {

        //public static ServiceProvider BuildServiceProvider(InputParameters inputParameters)
        //{
        //    return new ServiceCollection()
        //        .AddLogging(logging =>
        //        {
        //            //logging.AddConfiguration();
        //            logging.AddConsole();

        //        })
        //        .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug)

        //        .AddSingleton<IProcessManager, ProcessManager>() //Singleton? not if scale up...
        //        .AddScoped<IOutputConsoleManager, OutputConsoleManager>()

        //        //add all version checkers
        //        .AddScoped<IVersionChecker, ComputerChecker>()
        //        .AddScoped<IVersionChecker, OsVersionChecker>()
        //        .AddScoped<IVersionChecker, NetCoreRuntimeVersionChecker>()

        //        .AddScoped<IVersionChecker, NetCoreSdkVersionChecker>()
        //        .AddScoped<IVersionChecker, NetVersionChecker>()
        //        .AddScoped<IVersionChecker, NodeVersionChecker>()
        //        .AddScoped<IVersionChecker, NpmVersionChecker>() // Note: Not working. Why? It's in path...
        //        //.AddScoped<IVersionChecker, PsVersionChecker>()

        //        .BuildServiceProvider();
        //}
    }
}
