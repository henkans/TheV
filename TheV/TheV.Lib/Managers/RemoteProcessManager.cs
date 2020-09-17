using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using TheV.Lib.Helpers;
using TheV.Lib.Models;

namespace TheV.Lib.Managers
{
    public class RemoteProcessManager : IProcessManager
    {
        private static string Servername { get; set; }
        public bool Impersonate { get; }

        public string DomainName { get; }






        public RemoteProcessManager()
        {
            
        }

        

        
        public string RunCommand(string fileName, string arguments)  // Run synchronous 
        {

            RunPowershellScript($"{fileName}.exe {arguments}");


            string output = string.Empty;
            //var process = new Process();
            //try
            //{
            //    //Run process
            //    process.StartInfo.FileName = fileName;
            //    process.StartInfo.Arguments = arguments;
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.StartInfo.RedirectStandardError = true;
            //    process.Start();
            //}
            //catch (Win32Exception e)
            //{
            //    Debug.WriteLine(e);
            //    //throw new CheckerException($"File not found.", e);
            //    //https://docs.microsoft.com/sv-se/windows/win32/seccrypto/common-hresult-values
            //    //https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d
            //    if (e.ErrorCode == ErrorFail && e.NativeErrorCode == NativeErrorFileNotFound)
            //    {
            //        // File not found
            //        throw new CheckerException($"{fileName} not found.", e);
            //    }

            //    // This is something else
            //    throw new CheckerException(e.Message, e);

            //}
            //catch (ObjectDisposedException e)
            //{
            //    throw new CheckerException($"Process is disposed.", e);
            //}
            //catch (InvalidOperationException e)
            //{
            //    throw new CheckerException($"No file name was specified in the Process component's StartInfo.", e);
            //    // OR - The ProcessStartInfo.UseShellExecute member of the StartInfo property is true while ProcessStartInfo.RedirectStandardInput, ProcessStartInfo.RedirectStandardOutput, or ProcessStartInfo.RedirectStandardError is true.
            //}
            
            //try
            //{
            ////Read output (or error)
            //    output = process.StandardOutput.ReadToEnd();
            //    Debug.WriteLine(output);
            //    string err = process.StandardError.ReadToEnd();
            //    if (!string.IsNullOrEmpty(err)) throw new ArgumentException($"RunCommand Error {fileName} - args: {arguments} ");

            //    process.WaitForExit();
            //}
            //catch (ArgumentException e)
            //{
            //    Debug.WriteLine(e);
            //    throw new CheckerException($"Run command error '{fileName} {arguments}'.", e);
            //    //throw;
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e);
            //    throw;
            //}
            return output;
        }

        private bool RunPowershellCommand(Command command)
        {
            bool success;

            Runspace runspace = RunspaceFactory.CreateRunspace(GetWSManConnectionInfo());
            runspace.Open();
            Pipeline pipeLine = runspace.CreatePipeline();

            pipeLine.Commands.Add(command);
            try
            {
                var results = pipeLine.Invoke();

                // Errorhandling cmdlets
                if (pipeLine.Error.Count > 0)
                {
                    var error = pipeLine.Error.Read();
                    if (error is ErrorRecord)
                    {
                        throw new CmdletInvocationException($"[PowerShell]: Error in cmdlet: {((ErrorRecord)error).Exception.Message}");
                    }
                }

                // Veiw Output in debug
                if (results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        if (result.Properties["Name"] != null)
                            Debug.WriteLine(result.Properties["Name"].Value);
                    }

                }

                success = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                success = false;
            }
            finally
            {
                pipeLine.Dispose();
                runspace.Dispose();
            }
            return success;
        }

        private bool RunPowershellScript(string script)
        {
            bool success;
            Runspace runspace = RunspaceFactory.CreateRunspace(GetWSManConnectionInfo());
            runspace.Open();
            Pipeline pipeLine = runspace.CreatePipeline();

            pipeLine.Commands.AddScript(script);

            try
            {
                var results = pipeLine.Invoke();

                // Errorhandling cmdlets
                if (pipeLine.Error.Count > 0)
                {
                    var err = new StringBuilder();
                    err.AppendLine("[PowerShell]: Error in cmdlet:");

                    while (!pipeLine.Error.EndOfPipeline)
                    {
                        var readError = pipeLine.Error.Read();

                        if (readError is Collection<ErrorRecord>)
                        {
                            var records = readError as Collection<ErrorRecord>;
                            foreach (var error in records)
                            {
                                err.AppendLine(error.Exception.Message);
                            }
                        }

                        if (readError is ErrorRecord)
                        {
                            var record = readError as ErrorRecord;
                            err.AppendLine(record.Exception.Message);
                        }
                    }

                    throw new CmdletInvocationException(err.ToString());
                }

                // Veiw Output in debug
                if (results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        if (result.Properties["Name"] != null)
                            Debug.WriteLine(result.Properties["Name"].Value);
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                success = false;
            }
            finally
            {
                pipeLine.Dispose();
                runspace.Dispose();
            }
            return success;
        }

        private WSManConnectionInfo GetWSManConnectionInfo()
        {
            WSManConnectionInfo connectionInfo;
            
                connectionInfo =
                    new WSManConnectionInfo(false, Servername, 5985, null, null, PSCredential.Empty)
                    {
                        AuthenticationMechanism = AuthenticationMechanism.Kerberos,
                        SkipRevocationCheck = true
                    };

            return connectionInfo;
        }
    }
}