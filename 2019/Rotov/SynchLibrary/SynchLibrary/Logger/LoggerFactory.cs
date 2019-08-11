using System;
namespace SynchLibrary
{
    public static class LoggerFactory
    {
        public static ILogger Create(int type)
        {
            switch(type)
            {
                case 1:
                    return new SummaryLogger();
                case 2:
                    return new VerboseLogger();
            }
            return new SilentLogger();
        }
    }
}
