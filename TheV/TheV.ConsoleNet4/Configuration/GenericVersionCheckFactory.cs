using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheV.Lib.Models;

namespace TheV.ConsoleNet4.Configuration
{
    public class GenericVersionCheckFactory : IGenericVersionCheckFactory
    {

        public IList<GenericVersionCheck> Create()
        {
            var result = new List<GenericVersionCheck>();
            try
            {
                var config = TheVConfigurationSection.GetConfig();

                foreach (var item in config.VersionChecks)
                {
                    result.Add(new GenericVersionCheck
                    {
                        Title = ((VersionCheckElement) item).Title,
                        Name = ((VersionCheckElement)item).Name,
                        Filename = ((VersionCheckElement)item).Filename,
                        Arguments = ((VersionCheckElement)item).Arguments

                    });
                }

                return result;
            }
            catch (ConfigurationErrorsException e)
            {
                if (!Console.IsOutputRedirected) Console.Clear();
                //ConsoleOutput.Write($"Configuration error: {e.BareMessage}", 1);
                Debug.WriteLine($"Configuration error:\n {e}");
                return null;
                //Environment.Exit(-1);
            }
        }
    }
}
