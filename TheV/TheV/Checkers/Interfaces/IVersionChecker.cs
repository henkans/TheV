namespace TheV.Checkers.Interfaces
{
    public interface IVersionChecker
    {
        string GetVersion(bool verbose = false);
    }
}
