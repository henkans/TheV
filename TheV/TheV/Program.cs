using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheV.Checkers;
using TheV.Checkers.Interfaces;
using TheV.Managers;
using TheV.Models;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;


namespace TheV
{
    class Program
    {

        //private static OutputTypes _outputType = OutputTypes.Normal;
        private static ServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {


            // Get command line parameters
            RootCommand rootCommand = new RootCommand("TheV - Version checker");
            Option verboseOption = new Option(new string[] { "--verbose", "-v" }, "Run verbose mode");
            Option debugOption = new Option(new string[] { "--debug", "-d" }, "Run debug mode");

            rootCommand.AddOption(verboseOption);
            rootCommand.AddOption(debugOption);
            //Option outputOption = new Option(new string[] { "--output", "-o" }, description: "The target name of the output file.");
            //rootCommand.AddOption(outputOption);



            //    //// Note that the parameters of the handler method are matched according to the names of the options
            //    //rootCommand.Handler = CommandHandler.Create<int, bool, FileInfo>((intOption, boolOption, fileOption) =>
            //    //{
            //    //    Console.WriteLine($"The value for --int-option is: {intOption}");
            //    //    Console.WriteLine($"The value for --bool-option is: {boolOption}");
            //    //    Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
            //    //});

            //    //// Parse the incoming args and invoke the handler
            //    //var temp =rootCommand.InvokeAsync(args).Result;

            rootCommand.Handler = CommandHandler.Create<bool, bool>(
                (verbose, debug) =>
                {
                    _serviceProvider = BuildServiceProvider(new InputParameters(verbose, debug));
                    RunVersionCheckers(new InputParameters(verbose, debug));
                });

            await rootCommand.InvokeAsync(args);

        }


        public static void RunVersionCheckers(InputParameters inputParameters)
        {
            var outputConsoleManager = _serviceProvider.GetRequiredService<IOutputConsoleManager>();
            outputConsoleManager.WriteHeader(inputParameters);
            
            // Run all version checkers
            var services = _serviceProvider.GetServices<IVersionChecker>();
            foreach (var service in services)
            {
                try
                {
                    outputConsoleManager.WriteVersion(service, inputParameters);
                }
                finally
                {
                    service?.Dispose();
        
                }
                
            }

            // Logger
            var logger = _serviceProvider.GetService<ILogger<Program>>();
            logger.LogDebug("All done!");
            // logger.LogCritical("All done!");
            // logger.LogError("All done!");
            // logger.LogInformation("All done!");
            // logger.LogWarning("All done!");
            // logger.LogDebug("All done!");

        }



        public static ServiceProvider BuildServiceProvider(InputParameters inputParameters)
        {
            return new ServiceCollection()
                .AddLogging(logging =>
                {
                    //logging.AddConfiguration();
                    logging.AddConsole();

                })
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug)

                .AddSingleton<IProcessManager, ProcessManager>() //Singleton? not if scale up...
                .AddScoped<IOutputConsoleManager, OutputConsoleManager>()

                //add all version checkers
                .AddScoped<IVersionChecker, ComputerChecker>()
                // .AddScoped<IVersionChecker, OsVersionChecker>()
                .AddScoped<IVersionChecker, NetCoreRuntimeVersionChecker>()
 
                .AddScoped<IVersionChecker, NetCoreSdkVersionChecker>()
                .AddScoped<IVersionChecker, NetVersionChecker>()
                .AddScoped<IVersionChecker, NodeVersionChecker>()
                //.AddScoped<IVersionChecker, NpmVersionChecker>() // Note: Not working. Why? It's in path...
                //.AddScoped<IVersionChecker, PsVersionChecker>()

                .BuildServiceProvider();
        }
    }
}
