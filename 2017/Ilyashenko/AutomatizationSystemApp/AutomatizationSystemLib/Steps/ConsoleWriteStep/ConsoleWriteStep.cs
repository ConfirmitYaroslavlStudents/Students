using System;

namespace AutomatizationSystemLib
{
    public class ConsoleWriteStep : IStep
    {
        private ConsoleWriteOptions _options;

        public ConsoleWriteStep(ConsoleWriteOptions options)
        {
            _options = options;
        }

        public void Execute()
        {
            Console.WriteLine(_options.Message);
        }
    }
}
