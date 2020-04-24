using System.Runtime.InteropServices;
using TheV.Checkers.Interfaces;

namespace TheV.Checkers
{
    public class PsVersionChecker : IPsVersionChecker
    {
        private const string Tilte = "Powershell";
        public string GetVersion(bool verbose = false)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //Windows
                string regval = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\3", "Install", null).ToString();
                if (regval.Equals("1"))
                {
                    var regval2 = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine", "PowerShellVersion", null).ToString();
                    return $"{Tilte}:\n{regval2}";
                }
            }
            return string.Empty;
        }
    }
}
