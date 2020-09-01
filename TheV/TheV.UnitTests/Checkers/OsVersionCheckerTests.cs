using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Lib.Checkers;

namespace TheV.UnitTests.Checkers
{

    [TestClass]
    public class OsVersionCheckerTests
    {
        [TestMethod]
        public void GetOsInfo()
        {
            var handler = new OsVersionChecker();
            var result = handler.GetVersion();
            Assert.IsTrue(result.Contains("Windows"));

        }

    }
}
