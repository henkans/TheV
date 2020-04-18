using System;
using System.Collections.Generic;
using System.Text;
using TheV.Managers;

namespace TheV.Handlers
{
    public interface INodeVersionHandler
    {
        string GetVersion();
    }
    
    public class NodeVersionHandler : INodeVersionHandler
    {
        private readonly IProcessManager _processManager;

        public NodeVersionHandler(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string GetVersion()
        {
            var versionNumber = _processManager.RunCommand("node", "--version");
            return $"node {versionNumber}";
        }

    }
}
