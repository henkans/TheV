using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TheV.Checkers.Interfaces;
using TheV.Models;

namespace TheV.Checkers
{
    internal class ComputerChecker : IVersionChecker
    {
        private InputParameters _inputParameters;
        public string Title => "Computer";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;
            var versionResults = new Collection<VersionCheck>
            {
                new VersionCheck("Machine", Environment.MachineName),
                new VersionCheck("Ip", LocalIpAddress().ToString()),
                new VersionCheck("Mac", MacAddress())
            };
            return versionResults;
        }

        private IPAddress LocalIpAddress()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) return null;
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private string MacAddress()
        {
            return  NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
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
