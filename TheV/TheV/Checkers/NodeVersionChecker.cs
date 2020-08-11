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

        public NodeVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => "Node";

        public IEnumerable<CheckerResult> GetVersion(InputParameters inputParameters)
        {
            try
            {
                var versionNumber = _processManager.RunCommand("node", "--version");
                var versionResults = new Collection<CheckerResult>
                {
                    new CheckerResult(Title, versionNumber)
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
            Console.WriteLine("- {0} was disposed!", this.GetType().Name);
        }

    }
}
