using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TheV.ConsoleNet4.Configuration;
using TheV.Lib.Checkers;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.ConsoleNet4
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option('d', "debug", Required = false, HelpText = "Output debug messages.")]
            public bool Debug { get; set; }

            [Option('m', "machine", Required = false, HelpText = "Remote machine.")]
            public string Machine { get; set; }
        }


        static void Main(string[] args)
        {
            // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            CommandLine.Parser.Default.ParseArguments<Options>(args).MapResult((opts) =>
            {
                _serviceProvider = BuildServiceProvider(new InputParameters(opts.Verbose, opts.Debug));



                return RunVersionCheckers(new InputParameters(opts.Verbose, opts.Debug));
                    //return RunOptionsAndReturnExitCode(opts);

                }, //in case parser sucess
                errs => HandleParseError(errs)); //in  case parser fail




            Console.ReadKey();

        }
        static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = -2;
            Console.WriteLine("errors {0}", errs.Count());
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            Console.WriteLine("Exit code {0}", result);
            return result;
        }



        public static int RunVersionCheckers(InputParameters inputParameters)
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

            outputConsoleManager.WriteFooter(inputParameters);

            // Logger
            if (inputParameters.Verbose)
            {

            }

            return 0;

        }

        public static ServiceProvider BuildServiceProvider(InputParameters inputParameters)
        {
            //generic checkers
            var temp = new GenericVersionCheckFactory().Create();


           // var conf = new GenericVersionCheck { Title = "Generic Test", Name = "node", Filename = "node", Arguments = " --version" };

            var serviceCollection =   new ServiceCollection()

                .AddSingleton<IProcessManager, ProcessManager>() //Singleton? not if scale up...
                .AddScoped<IOutputConsoleManager, OutputConsoleManager>()



                //add all version checkers
                .AddScoped<IVersionChecker, ComputerChecker>()
                .AddScoped<IVersionChecker, OsVersionChecker>()
                .AddScoped<IVersionChecker, VisualStudioVersionChecker>()
                .AddScoped<IVersionChecker, NetCoreRuntimeVersionChecker>()

                .AddScoped<IVersionChecker, NetCoreSdkVersionChecker>()
                .AddScoped<IVersionChecker, NetVersionChecker>()
                .AddScoped<IVersionChecker, NodeVersionChecker>()
                .AddScoped<IVersionChecker, NpmVersionChecker>() 
                .AddScoped<IVersionChecker, PsVersionChecker>();

                // Add generics from config
                foreach (var genericVersionCheck in temp)
                {
                    serviceCollection.AddScoped<IVersionChecker, GenericVersionChecker>(x =>
                        new GenericVersionChecker(x.GetRequiredService<IProcessManager>(), genericVersionCheck));

                }



            return serviceCollection.BuildServiceProvider();


            //return new ServiceCollection()

            //    .AddSingleton<IProcessManager, ProcessManager>() //Singleton? not if scale up...
            //    .AddScoped<IOutputConsoleManager, OutputConsoleManager>()

            //    .AddScoped<IVersionChecker, GenericVersionChecker>(x => new GenericVersionChecker(x.GetRequiredService<IProcessManager>(), conf))



            //    //add all version checkers
            //    .AddScoped<IVersionChecker, ComputerChecker>()
            //    .AddScoped<IVersionChecker, OsVersionChecker>()
            //    .AddScoped<IVersionChecker, VisualStudioVersionChecker>()
            //    .AddScoped<IVersionChecker, NetCoreRuntimeVersionChecker>()

            //    .AddScoped<IVersionChecker, NetCoreSdkVersionChecker>()
            //    .AddScoped<IVersionChecker, NetVersionChecker>()
            //    .AddScoped<IVersionChecker, NodeVersionChecker>()
            //    .AddScoped<IVersionChecker, NpmVersionChecker>() // Note: Not working. Why? It's in path...
            //    .AddScoped<IVersionChecker, PsVersionChecker>()

            //    .BuildServiceProvider();
        }


    }
}
