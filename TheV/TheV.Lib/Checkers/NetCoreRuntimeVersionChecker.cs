using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{
    public class NetCoreRuntimeVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private InputParameters _inputParameters;

        public NetCoreRuntimeVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public string Title => ".NET Core runtime";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            // Init parameters
            _inputParameters = inputParameters;

            // Run  command
            var versions = _processManager.RunCommand("dotnet", "--list-runtimes");

            // Get rows
            string[] splittedVersions = versions.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Rest 
            //return GetVersionList(splittedVersions);
            var versionDic = new Dictionary<string, Version>();
            var result = new Collection<VersionCheck>();

            foreach (var row in splittedVersions)
            {
                string[] words = row.Split(' ');
                if (_inputParameters.Verbose)
                {
                    result.Add(new VersionCheck(words[0], words[1]));
                }
                else if (versionDic.TryGetValue(words[0], out Version existingVersion) && !_inputParameters.Verbose)
                {
                    var newVersion = new Version(words[1]);
                    if (newVersion > existingVersion)
                    {
                        versionDic[words[0]] = newVersion;
                    }
                }
                else
                {
                    versionDic.Add(words[0], new Version(words[1]));
                }
            }

            
            // convert to ICollection<VersionCheck>
            if (!_inputParameters.Verbose)
            {
                foreach (var version in versionDic)
                {
                    result.Add(new VersionCheck(version.Key, version.Value.ToString()));
                }
            }


            return result;



        }
        
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~A() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            if (_inputParameters.Debug)
            {
                Console.WriteLine($"debug: {GetType().Name} was disposed!");
            }

            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion 

    }
}