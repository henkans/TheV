using System.Collections.Generic;
using System.Linq;
using TheV.Managers;

namespace TheV.UnitTests.Mocks
{
    internal class ProcessManagerMock : IProcessManager
    {
        public List<ProcessManagerMockInput> MockInput;

        public ProcessManagerMock()
        {
            MockInput = new List<ProcessManagerMockInput>();
        }

        public ProcessManagerMock(List<ProcessManagerMockInput> mockInput)
        {
            MockInput = mockInput;
        }

        public void AddExpectation(string filename, string arguments, string returnValue)
        {
            var processManagerMockInput = new ProcessManagerMockInput(filename, arguments, returnValue);
            MockInput.RemoveAll(x => x.FileName == processManagerMockInput.FileName && x.Arguments == processManagerMockInput.Arguments );
            MockInput.Add(processManagerMockInput);
        }

        public string RunCommand(string fileName, string arguments)
        {
            var returnValue = MockInput.FirstOrDefault(mi => mi.FileName == fileName && mi.Arguments == arguments)?.ReturnValue;
            return string.IsNullOrWhiteSpace(returnValue) ? string.Empty : returnValue;
        }
    }
}
