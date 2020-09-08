using System;
using System.ComponentModel;
using System.Diagnostics;
using TheV.Lib.Helpers;
using TheV.Lib.Models;

namespace TheV.Lib.Managers
{
    public class ProcessManager : IProcessManager
    {
        private readonly Process _process;
        public const int ErrorFail = unchecked((int)0x80004005);
        public const int NativeErrorFileNotFound = 0x2;

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
            var process = new Process();
            try
            {
                //Run process
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
            }
            catch (Win32Exception e)
            {
                Debug.WriteLine(e);
                //throw new CheckerException($"File not found.", e);
                //https://docs.microsoft.com/sv-se/windows/win32/seccrypto/common-hresult-values
                //https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d
                if (e.ErrorCode == ErrorFail && e.NativeErrorCode == NativeErrorFileNotFound)
                {
                    // File not found
                    throw new CheckerException($"{fileName} not found.", e);
                }

                // This is something else
                throw new CheckerException(e.Message, e);

            }
            catch (ObjectDisposedException e)
            {
                throw new CheckerException($"Process is disposed.", e);
            }
            catch (InvalidOperationException e)
            {
                throw new CheckerException($"No file name was specified in the Process component's StartInfo.", e);
                // OR - The ProcessStartInfo.UseShellExecute member of the StartInfo property is true while ProcessStartInfo.RedirectStandardInput, ProcessStartInfo.RedirectStandardOutput, or ProcessStartInfo.RedirectStandardError is true.
            }
            
            try
            {
            //Read output (or error)
                output = process.StandardOutput.ReadToEnd();
                Debug.WriteLine(output);
                string err = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(err)) throw new ArgumentException($"RunCommand Error {fileName} - args: {arguments} ");

                process.WaitForExit();
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine(e);
                throw new CheckerException($"Run command error '{fileName} {arguments}'.", e);
                //throw;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
            return output;
        }
    }
}