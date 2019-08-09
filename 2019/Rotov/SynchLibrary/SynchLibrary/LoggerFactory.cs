using System;
namespace SynchLibrary
{
    public static class LoggerFactory
    {
        public static ILogger Create(string type)
        {
            switch(type)
            {
                case "summary":
                    return new SummaryLogger();
                case "verbose":
                    return new VerboseLogger();
            }
            throw new FormatException("Incorrect logger key");
        }
    }
}
