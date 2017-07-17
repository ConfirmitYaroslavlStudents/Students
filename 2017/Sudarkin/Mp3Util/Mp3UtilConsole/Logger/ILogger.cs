namespace Mp3UtilConsole.Logger
{
    public interface ILogger
    {
        void Write(string message, LogStatus status);
    }
}