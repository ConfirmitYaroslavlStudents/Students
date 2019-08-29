namespace Logging
{
    public class LogManager
    {
        private Configuration configuration;

        public static Logger Logger { get; private set; } = new Logger();

        public Configuration Configuration
        {
            get
            {
                return configuration;
            }
            set
            {
                configuration = value;
                Logger.Configuration = configuration;
            }
        }



    }
}
