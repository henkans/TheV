namespace TheV.Checkers
{
    class PsVersionChecker
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


        /*
         *
         
        PS51> (Get-ItemProperty -Path HKLM:\SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine -Name 'PowerShellVersion').PowerShellVersion
5.1.17134.1
         

        PS51> [version](Get-ItemProperty -Path HKLM:\SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine -Name 'PowerShellVersion').PowerShellVersion

Major  Minor  Build  Revision
-----  -----  -----  --------
5      1      17134  1
         *
         */


    }
}
