﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{
    public class PsVersionChecker : IVersionChecker
    {
        private InputParameters _inputParameters;
        public string Title => "Powershell";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;

            var versionResults = new Collection<VersionCheck>();

            //Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                
                string regval = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\3", "Install", null).ToString();
                if (regval.Equals("1"))
                {
                    var regval2 = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine", "PowerShellVersion", null).ToString();
                    versionResults.Add(new VersionCheck(Title, regval2)); 
                }

            }
            return versionResults;



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
