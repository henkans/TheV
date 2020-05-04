using TheV.Checkers.Interfaces;
using TheV.Managers;

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

        public string GetVersion(bool verbose = false)
        {
            return _processManager.RunCommand("dotnet", "--list-sdks");
        }
    }
}
