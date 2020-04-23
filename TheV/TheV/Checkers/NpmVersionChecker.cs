using TheV.Checkers.Interfaces;
using TheV.Managers;

namespace TheV.Checkers
{

    public class NpmVersionChecker : INpmVersionChecker
    {
        private readonly IProcessManager _processManager;

        public NpmVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string GetVersion(bool verbose = true)
        {
            var versionNumber = _processManager.RunCommand("npm", "--version");
            return $"npm {versionNumber}";
        }
    }
}
