using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{

    public class NodeVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private InputParameters _inputParameters;

        public NodeVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => "Node";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;
            try
            {
                var versionNumber = _processManager.RunCommand("node", "--version");
                var versionResults = new Collection<VersionCheck>
                {
                    new VersionCheck(Title, versionNumber)
                };

                return versionResults;
                //return $"{versionNumber}";
            }
            catch (ArgumentException e)
            {
                //return new Collection<VersionCheck> {new VersionCheck(e.Message, string.Empty)}; 
                //Console.WriteLine(e);
                throw;
                //return $"node is not found.";
            }
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
