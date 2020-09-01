using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Lib.Checkers;
using TheV.Lib.Models;
using TheV.UnitTests.Mocks;

namespace TheV.UnitTests.Checkers
{
    [TestClass]
    public class NetCoreSdkVersionCheckerTests
    {
        private readonly ProcessManagerMock _processManagerMock;

        // TODO Test cases
        // No sdk exits. Error..

        public NetCoreSdkVersionCheckerTests()
        {
            _processManagerMock = new ProcessManagerMock();
            _processManagerMock.AddExpectation("dotnet", "--version", "3.1.401\r\n");
            _processManagerMock.AddExpectation("dotnet", "--list-sdks", "2.1.802 [C:\\Program Files\\dotnet\\sdk]\r\n2.2.402 [C:\\Program Files\\dotnet\\sdk]\r\n3.1.202 [C:\\Program Files\\dotnet\\sdk]\r\n3.1.401 [C:\\Program Files\\dotnet\\sdk]\r\n");
        }

        [TestMethod]
        public void GetNetSdkCoreVersion()
        {
            //Arrange
            var handler = new NetCoreSdkVersionChecker(_processManagerMock);
            _processManagerMock.AddExpectation("dotnet", "--version", "3.1.401\r\n");

            //Act
            var result = handler.GetVersion(new InputParameters(false, false)).ToArray();

            //Assert
            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(result.Any(x => x.Version == "3.1.401"));

        }

        [TestMethod]
        public void GetNetSdkCoreVersion_NotUsingNewestVersion()
        {
            //Arrange
            var handler = new NetCoreSdkVersionChecker(_processManagerMock);
            _processManagerMock.AddExpectation("dotnet", "--version", "3.1.202\r\n");

            //Act
            var result = handler.GetVersion(new InputParameters(false, false)).ToArray();

            //Assert
            Assert.IsTrue(result.Length == 2);
            Assert.IsTrue(result.Any(x => x.Version == "3.1.202  *In use"));

        }

        [TestMethod]
        public void GetNetCoreSdkVersion_Verbose()
        {
            //Arrange
            var handler = new NetCoreSdkVersionChecker(_processManagerMock);
            _processManagerMock.AddExpectation("dotnet", "--version", "3.1.401\r\n");

            //Act
            var result = handler.GetVersion(new InputParameters(true, false)).ToArray();

            //Assert
            // Assert.IsTrue(result.Any(x => x.Version == "3.1.401  *In use"));
            Assert.IsTrue(result.Length == 4);
            Assert.IsTrue(result.Any(x => x.Version.Contains("3.1.401  *In use")));
        }
    }
}
