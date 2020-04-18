using System;
using System.Collections.Generic;
using System.Text;

namespace TheV.Handlers
{
    class PsVersionHandler
    {


        // powershell.exe $PSVersionTable.Version

        public bool PowershellExists()
        {
            string regval = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\1", "Install", null).ToString();
            if (regval.Equals("1"))
                return true;
            else
                return false;
        }

    }
}
