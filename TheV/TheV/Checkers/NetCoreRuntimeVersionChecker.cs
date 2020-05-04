using TheV.Checkers.Interfaces;
using TheV.Managers;

namespace TheV.Checkers
{
    internal class NetCoreRuntimeVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;

        public NetCoreRuntimeVersionChecker(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public string Title => ".NET Core runtime";

        public string GetVersion(bool verbose = false)
        {

            return _processManager.RunCommand("dotnet", "--list-runtimes");

            //var resultCollection = new Collection<string>();

            //resultCollection.Add(_processManager.RunCommand("dotnet", "--version"));

            //return _processManager.RunCommand("dotnet", "--list-runtimes");

            // dotnet --list-sdks
            // dotnet --list-runtimes


            //npm list

            //npm --version
            //npm version

            //node --version   Node.js

            // dotnet --
            // dotnet --runtime

            // dotnet --info



            //var process = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = "dotnet",
            //        Arguments = "--version",  //--info
            //        UseShellExecute = false,
            //        RedirectStandardOutput = true,
            //        RedirectStandardError = true,
            //        CreateNoWindow = true
            //    }

            //};

            //process.OutputDataReceived += (sender, data) => {
            //    Console.WriteLine(data.Data);
            //    Debug.WriteLine(data.Data);
            //};

            //process.ErrorDataReceived += (sender, data) => {
            //    Console.WriteLine(data.Data);
            //    Debug.WriteLine(data.Data);
            //};


            //process.Start();


        }

        public string GetAllRuntimes()
        {
            return _processManager.RunCommand("dotnet", "--list-runtimes");

        }


        //public string GetAllSdks()
        //{
        //    return _processManager.RunCommand("--list-sdks");

        //}


    }
}