using System;
using System.Collections.Generic;
using System.Text;
using TheV.Managers;

namespace TheV.Handlers
{
    public interface INpmVersionHandler
    {
        string GetVersion();
    }

    public class NpmVersionHandler : INpmVersionHandler
    {
        private readonly IProcessManager _processManager;

        public NpmVersionHandler(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string GetVersion()
        {
            var versionNumber = _processManager.RunCommand("npm", "--version");
            return $"npm {versionNumber}";
        }
    }
}
