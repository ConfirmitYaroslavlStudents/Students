namespace Logging
{
    public interface ILogger
    {
        void Summary(string message);
        void Verbose(string message);
    }
}
