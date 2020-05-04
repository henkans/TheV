using System;
using TheV.Checkers.Interfaces;
using TheV.Managers;

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

        public string GetVersion(bool verbose = false)
        {
            try
            {
                var versionNumber = _processManager.RunCommand("node", "--version");
                return $"{versionNumber}";
            }
            catch (ArgumentException e)
            {
                //Console.WriteLine(e);
                //throw;
                return $"node is not found.";
            }
        }

    }
}
