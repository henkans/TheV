using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheV.Checkers.Interfaces;
using TheV.Managers;
using TheV.Models;

namespace TheV.Checkers
{
    internal class NetCoreRuntimeVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;

        public NetCoreRuntimeVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public string Title => ".NET Core runtime";

        public IEnumerable<CheckerResult> GetVersion(InputParameters inputParameters)
        {
            //TODO Do verbose & Debug...

            //switch (outputType)
            //{
                
            //}


            var versions = _processManager.RunCommand("dotnet", "--list-runtimes");
            string[] splittedVersions = versions.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var versionResults = new Collection<CheckerResult>();
            var addName = true;

            foreach (var splittedVersion in splittedVersions)
            {
                var outputVersion = splittedVersion;
                
                // do not show path...
                if (inputParameters.Verbose)
                {
                    outputVersion = splittedVersion;
                }
                else
                {
                    string[] words = splittedVersion.Split(' ');
                    outputVersion = words[0] + ' ' + words[1];
                    
                }

                if (addName) versionResults.Add(new CheckerResult(Title, outputVersion));
                else versionResults.Add(new CheckerResult(string.Empty, outputVersion));
                addName = false;
            }


            //var versionResults = new Collection<CheckerResult>
            //{
            //    new CheckerResult(Title, _processManager.RunCommand("dotnet", "--list-runtimes"))
            //};

            return versionResults;


            //return _processManager.RunCommand("dotnet", "--list-runtimes");

            //var resultCollection = new Collection<string>();

            //resultCollection.Add(_processManager.RunCommand("dotnet", "--version"));

            //return _processManager.RunCommand("dotnet", "--list-runtimes");

            // dotnet --list-sdks
            // dotnet --list-runtimes


            //npm list

            //npm --version
            //npm version

            //node --version   Node.js

            // dotnet --
            // dotnet --runtime

            // dotnet --info



            //var process = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = "dotnet",
            //        Arguments = "--version",  //--info
            //        UseShellExecute = false,
            //        RedirectStandardOutput = true,
            //        RedirectStandardError = true,
            //        CreateNoWindow = true
            //    }

            //};

            //process.OutputDataReceived += (sender, data) => {
            //    Console.WriteLine(data.Data);
            //    Debug.WriteLine(data.Data);
            //};

            //process.ErrorDataReceived += (sender, data) => {
            //    Console.WriteLine(data.Data);
            //    Debug.WriteLine(data.Data);
            //};


            //process.Start();


        }

        public string GetAllRuntimes()
        {
            return _processManager.RunCommand("dotnet", "--list-runtimes");

        }


        //public string GetAllSdks()
        //{
        //    return _processManager.RunCommand("--list-sdks");

        //}


        public void Dispose()
        {
            Console.WriteLine("- {0} was disposed!", this.GetType().Name);
        }

    }
}