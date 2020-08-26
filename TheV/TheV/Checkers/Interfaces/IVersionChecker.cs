using System;
using System.Collections.Generic;
using TheV.Models;

namespace TheV.Checkers.Interfaces
{
    public interface IVersionChecker : IDisposable
    {
        string Title { get; }
        IEnumerable<VersionCheck> GetVersion(InputParameters inputParameters);
    }
}
