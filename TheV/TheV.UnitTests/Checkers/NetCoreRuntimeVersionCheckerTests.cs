using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Checkers;
using TheV.Managers;
using TheV.Models;

namespace TheV.UnitTests.Checkers
{
    [TestClass]
    public class NetCoreRuntimeVersionCheckerTests
    {
        [TestMethod]
        public void GetNetCoreVersion()
        {
            var handler = new NetCoreRuntimeVersionChecker(new ProcessManager());
            var result = handler.GetVersion(new InputParameters(false,false));

            Assert.IsTrue(result.Any(x => x.Version.Contains("3.1.6")));

        }

    }
}
