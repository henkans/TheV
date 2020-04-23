using TheV.Checkers.Interfaces;
using TheV.Managers;

namespace TheV.Checkers
{
    public class NetCoreSdkVersionChecker: INetCoreSdkVersionChecker
    {
        private readonly IProcessManager _processManager;

        public NetCoreSdkVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public string GetVersion(bool verbose = false)
        {
            return ".NET Core SDKs installed:\n" + _processManager.RunCommand("dotnet", "--list-sdks");
        }
    }
}
