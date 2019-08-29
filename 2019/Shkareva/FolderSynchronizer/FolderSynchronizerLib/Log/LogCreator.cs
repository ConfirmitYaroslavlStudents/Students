namespace FolderSynchronizerLib
{
    public class LogCreator
    {
        public ILog Create(LogLevels loglevel)
        {
            switch (loglevel)
            {
                case LogLevels.summary:
                    return new SummaryLog();
                case LogLevels.silent:
                    return new SilentLog();
                case LogLevels.verbose:
                    return new VerboseLog();
                default:
                    break;
            }
            return null;
        }
    }
}
