using System.Collections.Generic;

namespace Logging
{
    public class Configuration
    {
        private List<Rule> Rules { get; } = new List<Rule>();

        public void AddRule(LogLevel level, ITarget target)
        {
            Rules.Add(new Rule(level, target));
        }

        public List<ITarget> GetTargetsWithLevel(LogLevel level)
        {
            var result = new List<ITarget>();

            foreach (var rule in Rules)
            {
                if (rule.Level == level)
                {
                    result.Add(rule.Target);
                }
            }

            return result;
        }
    }
}
