using System.Diagnostics;

namespace TheV.UnitTests.Mocks
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    internal class ProcessManagerMockInput
    {
        public ProcessManagerMockInput(string fileName, string arguments, string returnValue)
        {
            FileName = fileName;
            Arguments = arguments;
            ReturnValue = returnValue;
        }

        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string ReturnValue { get; set; }

        private string DebuggerDisplay => $"{FileName} {Arguments}";
    }
}
