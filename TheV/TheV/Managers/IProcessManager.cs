using System;
using System.Collections.Generic;
using System.Text;

namespace TheV.Managers
{
    public interface IProcessManager
    {
        string RunCommand(string fileName, string arguments);
    }
}
