using System;
using System.Collections.Generic;
using System.Text;
using TheV.Checkers.Interfaces;

namespace TheV.Managers
{
    public class VersionCheckerManager : IVersionCheckerManager
    {
        private readonly IEnumerable<IVersionChecker> _versionCheckers;

        public VersionCheckerManager(IEnumerable<IVersionChecker> versionCheckers)
        {
            _versionCheckers = versionCheckers;
        }


        public void Run()
        {
            foreach (var versionChecker in _versionCheckers)
            {
                Console.WriteLine(versionChecker.GetVersion());
            }
        }



    }
}
