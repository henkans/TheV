using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Checkers;
using TheV.Managers;

namespace TheV.UnitTests.Handlers
{
    [TestClass]
    public class NetCoreVersionHandlerTests
    {
        [TestMethod]
        public void GetNetCoreVersion()
        {
            var handler = new NetCoreRuntimeVersionChecker(new ProcessManager());
            handler.GetVersion();

            // Assert.IsTrue(result.Caption.Contains("Windows"));

        }

    }
}
