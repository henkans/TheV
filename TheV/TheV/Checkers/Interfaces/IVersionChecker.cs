using System;
using System.Collections.Generic;
using TheV.Models;

namespace TheV.Checkers.Interfaces
{
    public interface IVersionChecker : IDisposable
    {
        string Title { get; }
        IEnumerable<CheckerResult> GetVersion(InputParameters inputParameters);
    }
}
