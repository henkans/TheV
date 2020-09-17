using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{
    public class VisualStudioVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private InputParameters _inputParameters;

        public VisualStudioVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public string Title => "Visual Studio";
        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;

            try
            {
                var productDisplayVersion = _processManager.RunCommand(@"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe", "-latest -property catalog_productDisplayVersion").Trim();
                var displayName = _processManager.RunCommand(@"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe", "-latest -property displayName").Trim();
                var versionResults = new Collection<VersionCheck>
                {
                    new VersionCheck($"{displayName}", productDisplayVersion)
                };

                return versionResults;
            }
            catch (ArgumentException e)
            {  //catalog_productDisplayVersion:
                //catalog_productLineVersion:
                // "C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" -latest -property
                //return new Collection<VersionCheck> {new VersionCheck(e.Message, string.Empty)}; 
                //Console.WriteLine(e);
                throw;
                //return $"node is not found.";
            }


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
                Console.WriteLine("- {0} was disposed!", this.GetType().Name);
            }

            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion 
    }
}
