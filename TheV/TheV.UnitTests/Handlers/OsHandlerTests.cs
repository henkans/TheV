using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Checkers;

namespace TheV.UnitTests.Handlers { 

[TestClass]
public class OsHandlerTests
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
