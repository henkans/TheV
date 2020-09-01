using System;
using System.Collections.Generic;
using TheV.Lib.Models;

namespace TheV.Lib.Checkers.Interfaces
{
    public interface IVersionChecker : IDisposable
    {
        string Title { get; }
        IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters);
    }
}
