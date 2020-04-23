using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using TheV.Checkers.Interfaces;

namespace TheV.Checkers
{
    public class NetVersionChecker : INetVersionChecker
    {

        public string GetVersion(bool verbose = true)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Get Net version on Wondows from registry
                const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
                using var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey);
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    return $".NET Framework Version:\n {CheckFor45PlusVersion((int) ndpKey.GetValue("Release"))}\n";
                }
                return ".NET Framework Version 4.5 or later is not detected.";
            }

            // If linux return empty
            return string.Empty;


        }


        // Checking the version using >= enables forward compatibility.
        private string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 528040) return "4.8";
            if (releaseKey >= 461808) return "4.7.2";
            if (releaseKey >= 461308) return "4.7.1";
            if (releaseKey >= 460798) return "4.7";
            if (releaseKey >= 394802) return "4.6.2";
            if (releaseKey >= 394254) return "4.6.1";
            if (releaseKey >= 393295) return "4.6";
            if (releaseKey >= 379893) return "4.5.2";
            if (releaseKey >= 378675) return "4.5.1";
            if (releaseKey >= 378389) return "4.5";
            // A non-null release key means that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }




        public IList<string> GetOlderVersions()
        {
            // Opens the registry key for the .NET Framework entry.
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                var netVersions = new List<string>();

                foreach (var versionKeyName in ndpKey.GetSubKeyNames())
                {
                    // Skip .NET Framework 4.5 version information.
                    if (versionKeyName == "v4")
                    {
                        continue;
                    }

                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        // Get the .NET Framework version value.
                        var name = (string)versionKey.GetValue("Version", "");
                        // Get the service pack (SP) number.
                        var sp = versionKey.GetValue("SP", "").ToString();

                        // Get the installation flag, or an empty string if there is none.
                        var install = versionKey.GetValue("Install", "").ToString();
                        if (string.IsNullOrEmpty(install)) // No install info; it must be in a child subkey.
                            Console.WriteLine($"{versionKeyName}  {name}");
                        else
                        {
                            if (!(string.IsNullOrEmpty(sp)) && install == "1")
                            {
                                //Console.WriteLine($"{versionKeyName}  {name}  SP{sp}");
                                netVersions.Add($"{versionKeyName}  {name}  SP{sp}");
                            }
                        }

                        if (!string.IsNullOrEmpty(name))
                        {
                            continue;
                        }

                        foreach (var subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (!string.IsNullOrEmpty(name)) sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (string.IsNullOrEmpty(install)) //No install info; it must be later.
                                netVersions.Add($"{versionKeyName}  {name}");

                            // Console.WriteLine($"{versionKeyName}  {name}");
                            else
                            {
                                if (!(string.IsNullOrEmpty(sp)) && install == "1")
                                {
                                    //Console.WriteLine($"{subKeyName}  {name}  SP{sp}");
                                    netVersions.Add($"{subKeyName}  {name}  SP{sp}");
                                }
                                else if (install == "1")
                                {
                                    //Console.WriteLine($"  {subKeyName}  {name}");
                                    netVersions.Add($"  {subKeyName}  {name}");
                                }
                            }
                        }
                    }
                }

                return netVersions;
            }
        }
    }
}
