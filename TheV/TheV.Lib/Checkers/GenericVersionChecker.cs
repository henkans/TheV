using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TheV.Lib.Checkers.Interfaces;
using TheV.Lib.Managers;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers
{
    // Used when check is added in config
    public class GenericVersionChecker : IVersionChecker
    {
        private readonly IProcessManager _processManager;
        private readonly GenericVersionCheck _genericVersionCheckConfiguration;
        private InputParameters _inputParameters;

        public GenericVersionChecker(IProcessManager processManager, GenericVersionCheck genericVersionCheckConfiguration)
        {
            _processManager = processManager;
            _genericVersionCheckConfiguration = genericVersionCheckConfiguration;
        }

        public string Title => _genericVersionCheckConfiguration.Title;

        public IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters)
        {
            _inputParameters = inputParameters;
            try
            {
                // TODO: retry

                var versionNumber = _processManager.RunCommand(_genericVersionCheckConfiguration.Filename, _genericVersionCheckConfiguration.Arguments).Trim(); //.Replace("v", "");
                var versionResults = new Collection<VersionCheck>
                {
                    new VersionCheck(Title, versionNumber)
                };

                return versionResults;
            }
            catch (ArgumentException e)
            {
                //return new Collection<VersionCheck> {new VersionCheck(e.Message, string.Empty)}; 
                //Console.WriteLine(e);
                throw;
                //return $"node is not found.";
            }
        }

        public void Dispose()
        {
            if (_inputParameters.Debug)
            {
                Console.WriteLine($"debug: {GetType().Name} was disposed!");
            }
        }

    }
}
