using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TheV.Checkers.Interfaces;
using TheV.Helpers;
using TheV.Models;

[assembly: InternalsVisibleTo("TheV.UnitTests")]
namespace TheV.Checkers
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


    internal class OsVersionChecker //: IVersionChecker
    {

        public string Title => "Operating System";

        public string GetVersion(bool verbose = true)
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
