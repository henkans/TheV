using System;
using System.Diagnostics;
using TheV.Managers;

namespace TheV.Handlers
{

    public interface INetCoreVersionHandler
    {
        string GetVersion();
    }

    public class NetCoreVersionHandler : INetCoreVersionHandler
    {
        private readonly IProcessManager _processManager;

        public NetCoreVersionHandler(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public string GetVersion()
        {
            return _processManager.RunCommand("dotnet","--version");

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