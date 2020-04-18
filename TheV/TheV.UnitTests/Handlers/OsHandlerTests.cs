using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Handlers;

namespace TheV.UnitTests.Handlers { 

[TestClass]
public class OsHandlerTests
{
    [TestMethod]
    public void GetOsInfo()
    {
        var handler = new OsVersionHandler();
        var result = handler.GetOsInfo();

        Assert.IsTrue(result.Caption.Contains("Windows"));

    }

}
}
