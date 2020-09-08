using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Helpers;
using TheV.Lib.Models;

[assembly: InternalsVisibleTo("TheV.UnitTests")]
namespace TheV.Lib.Checkers
{



    /*
     *
     using System.Runtime.InteropServices;

public static class OperatingSystem
{
    public static bool IsWindows() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static bool IsMacOS() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static bool IsLinux() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
}
     *
     */


    public class OsVersionChecker : IVersionChecker
    {
        private InputParameters _inputParameters;
        public string Title => "OS";

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;

            var versionResults = new Collection<VersionCheck>();

            // OS
            var osNameAndVersion = RuntimeInformation.OSDescription;
            var osVersion = string.Concat(osNameAndVersion.SkipWhile(c => !Char.IsDigit(c))).Trim();
            var osName = string.Concat(osNameAndVersion.TakeWhile(c => !Char.IsDigit(c))).Trim();
            versionResults.Add(new VersionCheck(osName, osVersion));

            // Architecture
            if (_inputParameters.Verbose)
            {
                var osArchitecture = RuntimeInformation.OSArchitecture;
                versionResults.Add(new VersionCheck("Architecture", osArchitecture.ToString()));
            }
; 

            return versionResults;

        }

        public void Dispose()
        {
            if (_inputParameters.Debug)
            {
                Console.WriteLine($"debug: {GetType().Name} was disposed!");
            }
        }

        public string GetVersion()
        {
            var os = new OS();
            if (IsWindows())
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                foreach (var managementObject in mos.Get())
                {
                    os = managementObject.MapOs();
                }
            }
            return os.Print();
        }


        /* LINUX */
        /*
         hostname

          cat /etc/os-release
         
         */

        public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
