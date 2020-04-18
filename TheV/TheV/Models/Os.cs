using System.Diagnostics;
using System.Text;

namespace TheV.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class OS
    {
        public string Caption { get; set; }
        public string Version { get; set; }
        public string BuildNumber { get; set; }
        public string ServicePack { get; set; }
        public string OSArchitecture { get; set; }
        public string Manufacturer { get; set; }
        public string ComputerName { get; set; }

        public string Print()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Caption:........{Caption}");
            stringBuilder.AppendLine($"Version:........{Version}");
            stringBuilder.AppendLine($"BuildNumber:....{BuildNumber}");
            stringBuilder.AppendLine($"ServicePack:....{ServicePack}");
            stringBuilder.AppendLine($"OSArchitecture:.{OSArchitecture}");
            stringBuilder.AppendLine($"Manufacturer:...{Manufacturer}");
            stringBuilder.AppendLine($"ComputerName:...{ComputerName}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return $"{Caption} {OSArchitecture} {Version}";
        }

        private string DebuggerDisplay => $"{Caption} {OSArchitecture} {Version}";
    }
}