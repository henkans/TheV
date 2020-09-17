using System.Collections.Generic;
using TheV.Lib.Models;

namespace TheV.ConsoleNet4.Configuration
{
    public interface IGenericVersionCheckFactory
    {
        IList<GenericVersionCheck> Create();
    }
}