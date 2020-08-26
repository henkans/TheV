using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheV.Checkers.Interfaces;
using TheV.Managers;
using TheV.Models;

namespace TheV.Checkers
{

    internal class NodeVersionChecker : IVersionChecker
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
