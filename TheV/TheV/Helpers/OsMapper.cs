using System;
using System.Management;
using TheV.Models;

namespace TheV.Helpers
{
    internal static class OsMapper
    {
        internal static OS MapOs(this object obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            var os = new OS();
            var managementObject = (ManagementObject)obj;
            if (managementObject["Caption"] != null) os.Caption = managementObject["Caption"].ToString();
            if (managementObject["Version"] != null) os.Version = managementObject["Version"].ToString();
            if (managementObject["BuildNumber"] != null) os.BuildNumber = managementObject["BuildNumber"].ToString();
            if (managementObject["Manufacturer"] != null) os.Manufacturer = managementObject["Manufacturer"].ToString();
            if (managementObject["OSArchitecture"] != null) os.OSArchitecture = managementObject["OSArchitecture"].ToString();
            if (managementObject["CSName"] != null) os.ComputerName = managementObject["CSName"].ToString();
            if (managementObject["CSDVersion"] != null) os.ServicePack = managementObject["CSDVersion"].ToString();
            return os;
        }
        

    }
}
