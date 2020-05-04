using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TheV.Models;

namespace TheV.Managers
{
    public class ProcessManagerSimple : IProcessManager
    {
        //private Process _shellProcess;

        // private Process ShellProcess { get { return _shellProcess = ShellProcess ?? new Process(); } }
        //private Process _shellProcess;

        public ProcessManagerSimple()
        {
            
        }

        public string RunCommand(string fileName, string arguments)
        {
            string output;
            try
            {
              
                var _shellProcess = new Process();
                _shellProcess.StartInfo.FileName = fileName;
                _shellProcess.StartInfo.Arguments = arguments;
                _shellProcess.StartInfo.UseShellExecute = false;
                _shellProcess.StartInfo.RedirectStandardOutput = true;
                _shellProcess.StartInfo.RedirectStandardError = true;
                _shellProcess.Start();

                //Read output (or error)
                output = _shellProcess.StandardOutput.ReadToEnd();
                Debug.WriteLine(output);
                string err = _shellProcess.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(err)) throw new ArgumentException($"RunCommand Error {fileName} - args: {arguments} ");

                _shellProcess.WaitForExit();
                //_shellProcess.Kill();
            }
            catch (Exception e)
            {

                throw;
            }

            return output;
        }


    }
}
