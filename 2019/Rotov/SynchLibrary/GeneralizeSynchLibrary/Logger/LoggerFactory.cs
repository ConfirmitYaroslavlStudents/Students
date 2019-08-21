using System;
namespace GeneralizeSynchLibrary
{
    public static class LoggerFactory
    {
        public static ILogger Create(LoggerConstant mode)
        {
            switch(mode)
            {
                case LoggerConstant.Summary:
                    return new SummaryLogger();
                case LoggerConstant.Verbose:
                    return new VerboseLogger();
            }
            return new SilentLogger();
        }
    }
}
