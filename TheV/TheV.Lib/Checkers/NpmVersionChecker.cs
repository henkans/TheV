using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{

    public class NpmVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private InputParameters _inputParameters;

        public NpmVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => "Npm";


        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;

            //try
            //{
            //TODO. do retry with cmd.exe /C
                var versionNumber = _processManager.RunCommand("cmd.exe", "/C npm --version").Trim();
                //var versionNumber = _processManager.RunCommand("npm", " --version");

                var versionResults = new Collection<VersionCheck>
                {
                    new VersionCheck(Title, versionNumber)
                };

                return versionResults;
            //}
            //catch (ArgumentException e)
            //{
            //    //return new Collection<VersionCheck> { new VersionCheck("Warning", e.Message) };
            //    //Console.WriteLine(e);
            //    throw;
            //    //return $"node is not found.";
            //}

            //throw new System.NotImplementedException();
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
