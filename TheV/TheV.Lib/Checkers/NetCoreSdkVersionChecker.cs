using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{
    public class NetCoreSdkVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private InputParameters _inputParameters;
        private const string InUseText = "  *In use";

        public NetCoreSdkVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public string Title => ".NET Core SDK";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;

            var versionInUse = _processManager.RunCommand("dotnet", "--version").Trim('\n').Trim('\r');
            var allVersions = _processManager.RunCommand("dotnet", "--list-sdks");
            string[] splittedVersions = allVersions.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Get newest....
            var versionDic = new Dictionary<Version, bool>();
            var result = new Collection<VersionCheck>();

            foreach (var row in splittedVersions)
            {
                string[] words = row.Split(' ');
                if (_inputParameters.Verbose)
                {
                    result.Add(new VersionCheck(string.Empty, (words[0] == versionInUse) ? words[0] + InUseText : words[0]));
                }
                else
                {
                    versionDic.Add(new Version(words[0]), false);
                }
            }

            if (!_inputParameters.Verbose)
            {
                var newest = versionDic.Max(v => v.Key);
                result.Add(new VersionCheck(string.Empty, newest.ToString()));
                if (newest.ToString() != versionInUse)
                {
                    var inUse = versionDic.FirstOrDefault(v => v.Key.ToString() == versionInUse).Key;
                    result.Add(new VersionCheck(string.Empty, inUse + InUseText));
                }
            }

            return result;
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
