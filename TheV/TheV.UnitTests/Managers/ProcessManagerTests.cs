using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheV.Managers;

namespace TheV.UnitTests.Managers
{
    [TestClass]
    public class ProcessManagerTests
    {
        [TestMethod]
        public void RunCommand_Get_Version()
        {
            var manager = new ProcessManager();
            var result = manager.RunCommand("dotnet", "--version");
            Assert.IsTrue(result.Length > 10);

        }

        [TestMethod]
        public void RunCommand_Get_Argument_Error()
        {
            var manager = new ProcessManager();
            var result = manager.RunCommand("dotnet", "argsthatnotexists");
            Assert.IsTrue(result.Length > 10);

        }

        [TestMethod]
        public void RunCommand_Get_Filename_Error()
        {
            var manager = new ProcessManager();
            var result = manager.RunCommand("appthatnotexists", "--version");
            Assert.IsTrue(result.Length > 10);

        }
    }
}
