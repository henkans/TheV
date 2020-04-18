using System;
using System.Management;
using TheV.Helpers;
using TheV.Models;

namespace TheV.Handlers
{
    public interface IOsVersionHandler
    {
        OS GetOsInfo();
    }


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


    public class OsVersionHandler : IOsVersionHandler
    {
        public OS GetOsInfo()
        {
            var os = new OS();
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (var managementObject in mos.Get())
            {
                os = managementObject.MapOs();
            }
            return os;
        }
    }
}
