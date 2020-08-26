using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheV.Checkers.Interfaces;
using TheV.Managers;
using TheV.Models;

namespace TheV.Checkers
{
    internal class NetCoreSdkVersionChecker: IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private InputParameters _inputParameters;


        public NetCoreSdkVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => ".NET Core SDK";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;


            var versionInUse = _processManager.RunCommand("dotnet", "--version").Trim('\n').Trim('\r');
            var allVersions = _processManager.RunCommand("dotnet", "--list-sdks");

            string[] splittedVersions = allVersions.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var versionResults = new Collection<VersionCheck>();

            foreach (var splittedVersion in splittedVersions)
            {
                var outputVersion = splittedVersion;

                // do not show path...
                if (!inputParameters.Verbose)
                {
                    string[] words = splittedVersion.Split(' ');

                    if (versionInUse == words[0])
                    {
                        outputVersion = words[0] + "  *In use";
                    }
                    else
                    {
                        outputVersion = words[0];
                    }

                    
                }

                versionResults.Add(new VersionCheck(string.Empty, outputVersion));
               
            }



            return versionResults;
        }

        public void Dispose()
        {
            if (_inputParameters.Debug)
            {
                Console.WriteLine($"debug: {GetType().Name} was disposed!");
            }
        }
    }
}
