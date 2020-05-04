namespace TheV.Checkers.Interfaces
{
    public interface IVersionChecker
    {
        string Title { get; }
        string GetVersion(bool verbose = false);
    }
}
