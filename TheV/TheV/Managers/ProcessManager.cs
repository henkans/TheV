using System;
using System.Diagnostics;
using TheV.Models;

namespace TheV.Managers
{

    public interface IProcessManager
    {
        string RunCommand(string fileName, string arguments);
        void RunCommandAsync(string fileName, string arguments);
        void WriteToStandardInput(string command);
        void Kill();
    }

    public class ProcessManager : IProcessManager
    {


        private readonly Process _process;

        public event EventHandler<DataEventArgs> OutputDataReceived = (sender, args) => { };
        public event EventHandler<DataEventArgs> ErrorDataReceived = (sender, args) => { };
        public event EventHandler Exited = (sender, args) => { };

        public bool IsRunning { get; private set; }
        public bool HasExited { get; private set; }
        public int ProcessId { get; private set; }
        public int ExitCode { get; private set; }

        public ProcessManager()
        {
            
        }
        public ProcessManager(string arguments = "")
        {
            var startInfo = new ProcessStartInfo()
            {
               
                FileName = "dotnet",
                Arguments = arguments,
                // WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
            };

            _process = new Process()
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true,
            };
            _process.OutputDataReceived += ProcessOutputDataReceived;
            _process.ErrorDataReceived += ProcessErrorDataReceived;
            _process.Exited += ProcessExited;
        }

        // Run command asynchronously...kind of
        public void RunCommandAsync(string fileName, string arguments = "")
        {
            if (!IsRunning && !HasExited)
            {


                if (_process.Start())
                {
                    IsRunning = true;
                    ProcessId = _process.Id;

                    _process.BeginOutputReadLine();
                    _process.BeginErrorReadLine();


                }

            }
        }

        public void Kill()
        {
            // Only kill this process
            _process.Kill();

        }

        // Write command to standard input.
        public void WriteToStandardInput(string command)
        {
            if (IsRunning && !HasExited)
            {
                _process.StandardInput.Write(command);
            }
        }

        // Handler for OutputDataReceived event of process.
        private void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OutputDataReceived(this, new DataEventArgs(e.Data));
        }

        // Handler for ErrorDataReceived event of process.
        private void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //ErrorDataReceived(this, new DataEventArgs(e.Data));
            ErrorDataReceived(this, new DataEventArgs("Finns inte"));
        }

        // Handler for Exited event of process.
        private void ProcessExited(object sender, EventArgs e)
        {
            HasExited = true;
            IsRunning = false;
            ExitCode = _process.ExitCode;
            _process.WaitForExit();
            Exited(this, e);
        }






        public string RunCommand(string fileName, string arguments)  // Run synchronous 
        {
            string output;
            try
            {
                //Run process
                Process process = new Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                //Read output (or error)
                output = process.StandardOutput.ReadToEnd();
                Debug.WriteLine(output);
                string err = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(err)) throw new ArgumentException($"RunCommand Error - args: {arguments} ");

                process.WaitForExit();
            }
            catch (Exception e)
            {

                throw;
            }


            return output;
        }




    }
}