using System.Diagnostics;

namespace TheV.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class VersionCheck
    {
        public VersionCheck(string name, string version)
        {
            Name = name;
            Version = version;
        }
        public string Name { get; set; }
        public string Version { get; set; }
        private string DebuggerDisplay => $"{Name} {Version}";
    }
}
