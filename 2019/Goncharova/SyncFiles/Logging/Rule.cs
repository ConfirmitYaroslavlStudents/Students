namespace Logging
{
    public class Rule
    {
        public LogLevel Level { get; }
        public ITarget Target { get; }

        public Rule(LogLevel level, ITarget target)
        {
            Level = level;
            Target = target;
        }
    }
}
