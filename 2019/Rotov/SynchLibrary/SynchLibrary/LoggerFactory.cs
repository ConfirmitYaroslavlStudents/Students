using System;
namespace SynchLibrary
{
    public static class LoggerFactory
    {
        public static ILogger Create(int type)
        {
            switch(type)
            {
                case 0:
                    return new SilentLogger();
                case 1:
                    return new SummaryLogger();
                case 2:
                    return new VerboseLogger();
            }
            throw new FormatException("Incorrect logger key");
        }
    }
}
