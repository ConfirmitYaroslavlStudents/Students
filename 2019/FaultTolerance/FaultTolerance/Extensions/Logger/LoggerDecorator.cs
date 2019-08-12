using System;
using System.Diagnostics;

namespace FaultTolerance.Extensions.Logger
{
    public class LoggerDecorator : IToleranceBuilder
    {
        private readonly IToleranceBuilder _decorated;

        public LoggerDecorator(IToleranceBuilder decorated)
        {
            _decorated = decorated;
        }

        public void Execute(Action action)
        {
            Debug.WriteLine($"===> Start executing ... ");
            _decorated.Execute(action);
            Debug.WriteLine($"===> Finished");
        }
    }
}
