namespace TheV.Models
{
    public class InputParameters
    {
        public bool Verbose { get; set; }
        public bool Debug { get; set; }

        public InputParameters(bool verbose, bool debug)
        {
            Verbose = verbose;
            Debug = debug;
        }
    }
}
