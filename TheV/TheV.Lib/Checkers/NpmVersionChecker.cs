using TheV.Lib.Managers;

namespace TheV.Lib.Checkers
{

    public class NpmVersionChecker //: IVersionChecker
    {
        private readonly IProcessManager _processManager;

        public NpmVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => "Npm";

        public string GetVersion(bool verbose = true)
        {
            var versionNumber = _processManager.RunCommand("npm", "--version");
            return $"npm {versionNumber}";
        }
    }
}
