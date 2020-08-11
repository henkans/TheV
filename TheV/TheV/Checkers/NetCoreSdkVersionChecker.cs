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


        public NetCoreSdkVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => ".NET Core SDK";

        public IEnumerable<CheckerResult> GetVersion(InputParameters inputParameters)
        {
            //var versionResults = new Collection<CheckerResult>
            //{
            //    new CheckerResult(Title, _processManager.RunCommand("dotnet", "--version"))
            //};


            var versionInUse = _processManager.RunCommand("dotnet", "--version").Trim('\n').Trim('\r');
  


            var versions = _processManager.RunCommand("dotnet", "--list-sdks");
            string[] splittedVersions = versions.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var versionResults = new Collection<CheckerResult>();
            var addName = true;

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

                if (addName) versionResults.Add(new CheckerResult(Title, outputVersion));
                else versionResults.Add(new CheckerResult(string.Empty, outputVersion));
                addName = false;
            }



            return versionResults;
        }

        public void Dispose()
        {
            Console.WriteLine("- {0} was disposed!", this.GetType().Name);
        }
    }
}
